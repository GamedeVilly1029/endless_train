using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrikeConcrete : ValueConcrete
{
    BaseActor _striker;
    public StrikeConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    int value,
    BaseActor striker
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _striker = striker;
    }

    public override IEnumerator ChildExecute()
    {
        BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(
            TurnProcessorInst,
            LevelMasterInst,
            TurnProcessorInst.CurrentActor
        );

        if (actorAhead != null)
        {
            yield return actorAhead.TakeBluntDamage(Value);
            yield return actorAhead.RunBeforeDamageStatuses();

            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.Strike);
            yield return GlobalLowLevelConcrete.Pause;
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
        }
        else
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.Strike);
            yield return GlobalLowLevelConcrete.Pause;
        }
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new StrikeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _striker);
    }
}