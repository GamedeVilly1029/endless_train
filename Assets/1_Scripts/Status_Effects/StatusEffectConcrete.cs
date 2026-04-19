using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
}