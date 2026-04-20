using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseActor : MonoBehaviour, IActor
{
    public DungeonMaster DungeonMasterInstance;
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
    public Stack<int> PositionCellIndexHistory{get;set;}
    public List<IAction> FightBasedActionHistory{get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTurn{get;set;}
    public List<IStatusEffect> StatusEffectsDuringTurn {get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTakingDamage {get;set;}
    Transform IActor.GraphicTransform {get {return GraphicTransformInstance;}set{}}
    public BasePatternPicker PatternPicker{get {return PatternPickerSetter;} set{}}

    public virtual void Initialize()
    {
        Debug.LogError("Base class Initialization was called - call concrete class initialization instead");
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
            DungeonMasterInstance.AllActors.Remove(this);
            DungeonMasterInstance.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
            Destroy(gameObject);
        }
    }

    public void AddActionToFightHistory()
    {
        if (FightBasedActionHistory == null)
        {
            FightBasedActionHistory = new()
            {
                DungeonMasterInstance.CurrentAction
            };
        }
        else
        {
            FightBasedActionHistory.Add(DungeonMasterInstance.CurrentAction);
        }
    }

    public IEnumerator RunBeforeDamageStatuses()
    {
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTakingDamage)
        {
            yield return statusEffect.ApplyStatusEffect(DungeonMasterInstance);
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
            yield return statusEffect.ApplyStatusEffect(DungeonMasterInstance);
        }
        foreach (IStatusEffect effect in effectsToDestroy)
        {
            effect.SelfDestroy(DungeonMasterInstance);
        }
    }

    public void InitializeCellIndexHistories(){
        PositionCellIndexHistory = new();
        PositionCellIndexHistory.Push(PositionCellIndex);
    }

    public void RunBeforeTurnStatuses()
    {
        foreach (IStatusEffect status in StatusEffectsBeforeTurn)
        {
            status.ApplyStatusEffect(DungeonMasterInstance);
        }
    }
}