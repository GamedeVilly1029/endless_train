using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    Transform Transform {get;set;}
    int PositionCellIndex {get;set;}
    Stack<int> PositionCellIndexHistory {get;set;}
    List<IAction> ActionRow {get;set;}
    RectTransform ActionRowPanel{get;set;}
    int HP {get;set;}
    bool IsFacingRight{get;set;}
    List<IAction> FightBasedActionHistory{get;set;}

    List<IStatusEffect> StatusEffectsForTurn{get;set;}
    List<IStatusEffect> StatusEffectsBeforeTakingDamage{get;set;}

    void TryToDie(int HP);
    void AddActionToFightHistory();
    IEnumerator TakeDamage(int damageToTake);
}