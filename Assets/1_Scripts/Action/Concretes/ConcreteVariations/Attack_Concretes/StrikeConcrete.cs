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
    BaseAction actionOfThisConcrete,
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
        Debug.Log("Strike was called");
        BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(
            TurnProcessorInst,
            LevelMasterInst,
            _striker
        );

        if (actorAhead != null)
        {
            yield return actorAhead.TakeBluntDamage(Value);
            yield return actorAhead.RunBeforeDamageStatuses();

            Object.FindAnyObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.Strike);
            yield return GlobalLowLevelConcrete.Pause;
            Object.FindAnyObjectByType<AudioMaster>().PlaySound("hit");
        }
        else
        {
            Object.FindAnyObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.Strike);
            yield return GlobalLowLevelConcrete.Pause;
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new StrikeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _striker);
    }
}