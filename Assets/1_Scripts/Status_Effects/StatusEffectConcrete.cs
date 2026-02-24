using UnityEngine;
using System.Collections;

public static class StatusEffectConcrete
{
    public static IEnumerator IncreaseDamageOfFirstAttackConcrete(DungeonMaster dungeonMaster, StatusEffectConstructElement element)
    {
        ActionConstructElement constructElement = StatusEffectLowLevelFunctionality.ReturnAttackConcrete(StatusEffectLowLevelFunctionality.ExtractCurrentActorActions(dungeonMaster));
        yield return constructElement.ConcreteValue += element.StatusConcreteValue;
    }

    public static IEnumerator TakeDamage(DungeonMaster dungeonMaster, StatusEffectConstructElement element)
    {
        yield return dungeonMaster.CurrentActor.TakeDamage(element.StatusConcreteValue);
    }
}