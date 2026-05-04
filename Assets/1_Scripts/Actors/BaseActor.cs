using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseActor : MonoBehaviour, IActor
{
    [HideInInspector] public TurnProcessor TurnProcessorInst;
    [HideInInspector] public LevelMaster LevelMasterInst;
    public RectTransform ActionRowPanelInstance;
    public TextMeshPro HPBarText;
    public SpriteRenderer SpriteRend;
    public ActionRow ActionRowSetter;
    public Transform GraphicTransformInstance;
    public BasePatternPicker PatternPickerSetter;

    public Transform TransformReference{get{return transform;}set{}}
    public ActionRow ActionRowInst {get{return ActionRowSetter;}set{}}
    public int MaxHP{get;set;}
    public int CurrentHP{get;set;}
    public int PositionCellIndex {get;set;}
    public bool IsDead {get;set;} = false;
    public Stack<int> PositionCellIndexHistory{get;set;}
    public List<IAction> FightBasedActionHistory{get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTurn{get;set;}
    public List<IStatusEffect> StatusEffectsDuringTurn {get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTakingDamage {get;set;}
    Transform IActor.GraphicTransform {get {return GraphicTransformInstance;}set{}}
    public BasePatternPicker PatternPicker{get {return PatternPickerSetter;} set{}}

    public virtual void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        Debug.LogError("Base class child Initialization version was called - call concrete class initialization instead");
    }

    private void BaseInitialize(TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;

        Debug.Log("Values of the turnProcessor and levelMaster of the BaseActor were assigned");

        StatusEffectsDuringTurn = new();
        StatusEffectsBeforeTurn = new();
        StatusEffectsBeforeTakingDamage = new();
    }
    public void Initialize(int cellIndex, float YRotation, int HP, TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        BaseInitialize(turnProcessor, levelMaster);
        InitializeChild(cellIndex, YRotation, HP);
    }

    private void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
    }

    public void TryToDie(int HP)
    {
        if (CurrentHP <= 0)
        {
            IsDead = true;
            ActionRowInst.Actions.Clear();
            LevelMasterInst.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
            gameObject.SetActive(false);
        }
    }

    public void AddActionToFightHistory()
    {
        if (FightBasedActionHistory == null)
        {
            FightBasedActionHistory = new()
            {
                TurnProcessorInst.CurrentAction
            };
        }
        else
        {
            FightBasedActionHistory.Add(TurnProcessorInst.CurrentAction);
        }
    }

    public IEnumerator RunBeforeDamageStatuses()
    {
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTakingDamage)
        {
            yield return statusEffect.ApplyStatusEffect();
        }
    }

    public IEnumerator SubtractDamageFromHP(int damageToTake)
    {
        yield return CurrentHP -= damageToTake;
    }

    public bool IsFacingRight()
    {
        float y = TransformReference.eulerAngles.y;
        return Mathf.Abs(Mathf.DeltaAngle(y, 0)) < 1f;
    }

    public IAction ReturnFirstActionInRow()
    {
        if (ActionRowInst.Actions.Count > 0)
        {
            return ActionRowInst.Actions[0];
        }
        else
        {
            return null;
        }
    }

    public IEnumerator TriggerTurnBasedStatusEffects()
    {
        List<IStatusEffect> effectsToDestroy = new();

        foreach (IStatusEffect statusEffect in StatusEffectsDuringTurn)
        {
            if (statusEffect.DestroyAfterApplication)
            {
                effectsToDestroy.Add(statusEffect);
            }
            yield return statusEffect.ApplyStatusEffect();
        }
        foreach (IStatusEffect effect in effectsToDestroy)
        {
            effect.SelfDestroy(StatusEffectsDuringTurn);
        }
    }

    public void InitializeCellIndexHistories(){
        PositionCellIndexHistory = new();
        PositionCellIndexHistory.Push(PositionCellIndex);
    }

    public IEnumerator RunBeforeTurnStatuses()
    {
        List<IStatusEffect> effectsToDestroy = new();
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTurn)
        {
            if (statusEffect.DestroyAfterApplication)
            {
                effectsToDestroy.Add(statusEffect);
            }
            yield return statusEffect.ApplyStatusEffect();
        }
        foreach (IStatusEffect effect in effectsToDestroy)
        {
            effect.SelfDestroy(StatusEffectsBeforeTurn);
        }
    }
}