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
    List<IStatusEffect> StatusEffectsDuringTurn{get;set;}
    List<IStatusEffect> StatusEffectsBeforeTakingDamage{get;set;}
    List<IStatusEffect> StatusEffectsBeforeTurn{get;set;}
    BasePatternPicker PatternPicker {get;set;}
    bool IsDead{get;set;}
    void Initialize(int cellIndex, float YRotation, int HP, TurnProcessor turnProcessor, LevelMaster levelMaster);
    void InitializeChild(int cellIndex, float YRotation, int HP);
    void TryToDie(int HP);
    void AddActionToFightHistory();
    IEnumerator RunBeforeTurnStatuses();
    IEnumerator RunBeforeDamageStatuses();
    IEnumerator SubtractDamageFromHP(int damageToTake);
    bool IsFacingRight();
    IAction ReturnFirstActionInRow();
    IEnumerator TriggerTurnBasedStatusEffects();
    void InitializeCellIndexHistories();
}