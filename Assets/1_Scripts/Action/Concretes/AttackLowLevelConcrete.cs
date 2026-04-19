using System.Collections;
using UnityEngine;

public static class AttackLowLevelConcrete
{
    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        IActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(dungeonMaster, casted);
        if (actorAhead != null)
        {
            yield return actorAhead.SubtractDamageFromHP(casted.ConcreteValue);
            yield return actorAhead.RunBeforeDamageStatuses();

            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(dungeonMaster.CurrentActor);
            yield return GlobalLowLevelConcrete.Pause;
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
            ParticlePlayer.StopStrike(dungeonMaster.CurrentActor);
        }
        else
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(dungeonMaster.CurrentActor);
            yield return GlobalLowLevelConcrete.Pause;
            ParticlePlayer.StopStrike(dungeonMaster.CurrentActor);
        }
    }
}