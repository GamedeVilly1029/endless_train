using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StatusEffectConcrete
{
    public static IEnumerator IncreaseDamageOfFirstAttackConcrete(DungeonMaster dungeonMaster, StatusEffectConstructElement element)
    {
        ActionConstructElement constructElement = StatusEffectLowLevelFunctionality.ReturnAttackConcrete(StatusEffectLowLevelFunctionality.ExtractCurrentActorActions(dungeonMaster));
        yield return constructElement.ConcreteValue += element.StatusConcreteValue;
    }
}
