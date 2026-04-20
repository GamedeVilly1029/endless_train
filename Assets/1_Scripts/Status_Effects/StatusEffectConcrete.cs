using UnityEngine;
using System.Collections;

public static class StatusEffectConcrete
{
    public static IEnumerator IncreaseDamageOfFirstAttackConcrete(DungeonMaster dungeonMaster, IStatusEffectConstructElement element)
    {
        ValueStatusEffectConstructElement casted = element as ValueStatusEffectConstructElement;
        ValueConstructElement constructElement = StatusEffectLowLevelFunctionality.ReturnAttackConcrete(StatusEffectLowLevelFunctionality.ExtractCurrentActorActions(dungeonMaster));
        if (constructElement!= null)
        {
            yield return constructElement.ConcreteValue += casted.StatusConcreteValue;
        }
        yield return null;
    }

    public static IEnumerator TakeDamage(DungeonMaster dungeonMaster, IStatusEffectConstructElement element)
    {
        ValueStatusEffectConstructElement casted = element as ValueStatusEffectConstructElement;
        yield return element.Actor.SubtractDamageFromHP(casted.StatusConcreteValue);
    }

    public static IEnumerator StunFirstPlayerAction(DungeonMaster dungeonMaster, IStatusEffectConstructElement element)
    {
        yield return GlobalLowLevelConcrete.Pause;

        ActionAssignmentStatusConstructElement casted = element as ActionAssignmentStatusConstructElement;
        if (dungeonMaster.Player.ActionRowInst.Actions.Count > 0)
        {
            ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(dungeonMaster.Player.ActionRowInst.Actions[0]);
            dungeonMaster.Player.ActionRowInst.Actions.RemoveAt(0);
        }

        IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(dungeonMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
        createdAction.UIRepresentation.transform.SetSiblingIndex(0);
        dungeonMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);

        Debug.Log("StunFirstPlayerAction status effect was executed");

        yield return GlobalLowLevelConcrete.Pause;
    }
}