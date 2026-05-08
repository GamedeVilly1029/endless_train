using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeConcrete : ValueConcrete
{
    public StrikeConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    int value
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
    }

    public override IEnumerator ChildExecute()
    {
        IActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(
            TurnProcessorInst,
            LevelMasterInst,
            TurnProcessorInst.CurrentActor
        );

        if (actorAhead != null)
        {
            yield return actorAhead.SubtractDamageFromHP(Value);
            yield return actorAhead.RunBeforeDamageStatuses();

            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(TurnProcessorInst.CurrentActor);
            yield return GlobalLowLevelConcrete.Pause;
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
            ParticlePlayer.StopStrike(TurnProcessorInst.CurrentActor);
        }
        else
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(TurnProcessorInst.CurrentActor);
            yield return GlobalLowLevelConcrete.Pause;
            ParticlePlayer.StopStrike(TurnProcessorInst.CurrentActor);
        }
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new StrikeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value);
    }
}