using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    Transform TransformReference {get;set;}
    Transform GraphicTransform {get;set;}
    int PositionCellIndex {get;set;}
    Stack<int> PositionCellIndexHistory {get;set;}
    ActionRow ActionRowInst {get;set;}
    int MaxHP{get;set;}
    int CurrentHP {get;set;}
    List<IAction> FightBasedActionHistory{get;set;}
    List<IStatusEffect> StatusEffectsForTurn{get;set;}
    List<IStatusEffect> StatusEffectsBeforeTakingDamage{get;set;}
    BasePatternPicker PatternPicker {get;set;}
    public void Initialize();
    void TryToDie(int HP);
    void AddActionToFightHistory();
    IEnumerator RunBeforeDamageStatuses();
    IEnumerator SubtractDamageFromHP(int damageToTake);
    bool IsFacingRight();
    IAction ReturnFirstActionInRow();
    IEnumerator TriggerTurnBasedStatusEffects();
    void InitializeCellIndexHistories();
}