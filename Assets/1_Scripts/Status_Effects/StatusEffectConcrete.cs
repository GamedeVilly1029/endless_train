using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StatusEffectConcrete
{
    public static IEnumerator IncreaseDamageOfFirstAttackConcrete(DungeonMaster dungeonMaster, StatusEffectConstructElement element)
    {
        ActionConstructElement constructElement = StatusEffectLowLevelFunctionality.ReturnAttackConcrete(StatusEffectLowLevelFunctionality.ExtractCurrentActorActions(dungeonMaster));
        if (constructElement!= null)
        {
            yield return constructElement.ConcreteValue += element.StatusConcreteValue;
        }
        yield return null;
    }

    public static IEnumerator TakeDamage(DungeonMaster dungeonMaster, StatusEffectConstructElement element)
    {
        yield return element.Actor.SubtractDamageFromHP(element.StatusConcreteValue);
    }
}