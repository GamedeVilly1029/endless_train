using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    [HideInInspector] public TurnProcessor TurnProcessorInst;
    [HideInInspector] public LevelMaster LevelMasterInst;
    [HideInInspector] public int MaxHP;
    [HideInInspector] public int CurrentHP;
    public int PositionCellIndex;
    [HideInInspector] public bool IsDead = false;
    [HideInInspector] public int Defense;

    public RectTransform ActionRowPanelInstance;

    public TextMeshPro HPBarText;
    public TextMeshPro DefenseBarText;

    public SpriteRenderer SpriteRend;
    public ActionRow ActionRowInst;
    public Transform GraphicTransform;
    public BasePatternPicker PatternPicker;
    public Transform TransformReference{get{return transform;}set{}}
    public Stack<int> PositionCellIndexHistory;
    public List<BaseAction> FightBasedActionHistory;
    public List<IStatusEffect> StatusEffectsBeforeTurn;
    public List<IStatusEffect> StatusEffectsDuringTurn; 
    public List<IStatusEffect> StatusEffectsBeforeTakingDamage;
    public List<ActorTrait> Traits;

    public IEnumerator TurnStart()
    {
        InitializeCellIndexHistories();
        yield return RunBeforeTurnStatuses();
    }

    public IEnumerator TurnEnd()
    {
        yield return Defense = 0;
    }

    public virtual void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        TransformReference.position = LevelMasterInst.Cells[cellIndex].CellPosition;
        LevelMasterInst.Cells[cellIndex].EnityOccupyingThisCell = this;
        PositionCellIndex = cellIndex;
        MaxHP = HP;
        CurrentHP = MaxHP;
        GraphicTransform.rotation = Quaternion.Euler(0f, YRotation, 0f);
    }

    private void BaseInitialize(TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;

        StatusEffectsDuringTurn = new();
        StatusEffectsBeforeTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        Traits = new();
    }
    public void Initialize(int cellIndex, float YRotation, int HP, TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        BaseInitialize(turnProcessor, levelMaster);
        InitializeChild(cellIndex, YRotation, HP);
        PatternPicker.Initialize(this);
    }

    public void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
        DefenseBarText.text = Defense.ToString();
    }

    public void TryToDie(int HP)
    {
        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        IsDead = true;
        ActionRowInst.Actions.Clear();
        LevelMasterInst.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
        gameObject.SetActive(false);
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
            yield return statusEffect.Apply();
        }
    }

    public IEnumerator TakeBluntDamage(int damageToTake)
    {
        if (damageToTake > Defense)
        {
            int leftDamage = damageToTake - Defense;
            yield return CurrentHP -= leftDamage;
            Defense = 0;
        }
        else if (damageToTake < Defense)
        {
            Defense -= damageToTake;
        }
        else
        {
            Defense = 0;
        }
    }

    public IEnumerator TakePiercingDamage(int damageToTake)
    {
        yield return CurrentHP -= damageToTake;
    }



    public bool IsFacingRight()
    {
        float y = GraphicTransform.eulerAngles.y;
        return Mathf.Abs(Mathf.DeltaAngle(y, 0)) < 1f;
    }

    public BaseAction ReturnFirstActionInRow()
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

    public IEnumerator RunDuringTurnStatuses()
    {
        List<IStatusEffect> effectsToDestroy = new();

        foreach (IStatusEffect statusEffect in StatusEffectsDuringTurn)
        {
            if (statusEffect.DestroyAfterApplication)
            {
                effectsToDestroy.Add(statusEffect);
            }
            yield return statusEffect.Apply();
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
            yield return statusEffect.Apply();
        }
        foreach (IStatusEffect effect in effectsToDestroy)
        {
            effect.SelfDestroy(StatusEffectsBeforeTurn);
        }
    }
}