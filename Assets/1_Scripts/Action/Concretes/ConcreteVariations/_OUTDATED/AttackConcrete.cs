using System.Collections;
using UnityEngine;

public static class AttackConcrete
{
    private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static IEnumerator StrikeConcrete(IConstructElement element)
    // {
    //     ValueConstructElement casted = element as ValueConstructElement;
    //     IActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor);
    //     if (actorAhead != null)
    //     {
    //         yield return actorAhead.SubtractDamageFromHP(casted.ConcreteValue);
    //         yield return actorAhead.RunBeforeDamageStatuses();

    //         Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
    //         ParticlePlayer.StartStrike(_turnProcessor.CurrentActor);
    //         yield return GlobalLowLevelConcrete.Pause;
    //         Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
    //         ParticlePlayer.StopStrike(_turnProcessor.CurrentActor);
    //     }
    //     else
    //     {
    //         Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
    //         ParticlePlayer.StartStrike(_turnProcessor.CurrentActor);
    //         yield return GlobalLowLevelConcrete.Pause;
    //         ParticlePlayer.StopStrike(_turnProcessor.CurrentActor);
    //     }
    // }

    // public static IEnumerator TantrumStrike(IConstructElement element)
    // {
    //     for (int i = _turnProcessor.CurrentActor.FightBasedActionHistory.Count - 1; i >= 0; i--)
    //     {
    //         if (_turnProcessor.CurrentActor.FightBasedActionHistory[i] is Tantrum)
    //         {
    //             yield return StrikeConcrete(element);
    //         }
    //         else
    //         {
    //             break;
    //         }
    //     }
    //     yield return StrikeConcrete(element);
    // }
}