using UnityEngine;
using System.Collections;

public static class StatusEffectConcrete
{
    // public static IEnumerator IncreaseDamageOfFirstAttackConcrete()
    // {
    //     ValueConcrete toEnhance = StatusEffectLowLevelFunctionality.ReturnAttackConcrete(StatusEffectLowLevelFunctionality.ExtractActorActions(TurnProcessorInstance.CurrentActor));
    //     if (toEnhance != null)
    //     {
    //         yield return toEnhance.Value += Value;
    //     }
    // }

    // public static IEnumerator TakeDamage(TurnProcessor turnProcessor, LevelMaster levelMaster)
    // {
    //     yield return element.Actor.SubtractDamageFromHP(casted.Value);
    // }

    // public static IEnumerator StunFirstPlayerAction(TurnProcessor turnProcessor, LevelMaster levelMaster, IStatusConcrete element)
    // {
    //     yield return GlobalLowLevelConcrete.Pause;

    //     ActionAddStatusConcrete casted = element as ActionAddStatusConcrete;
    //     if (levelMaster.Player.ActionRowInst.Actions.Count > 0)
    //     {
    //         ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(levelMaster.Player.ActionRowInst.Actions[0]);
    //     }

    //     IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(levelMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
    //     createdAction.UIRepresentation.transform.SetSiblingIndex(0);
    //     levelMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);


    //     yield return GlobalLowLevelConcrete.Pause;
    // }
}