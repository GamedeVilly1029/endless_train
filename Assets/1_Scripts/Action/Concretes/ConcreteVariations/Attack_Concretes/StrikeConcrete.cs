using System.Collections;
using System.Collections.Generic;
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
        BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(
            TurnProcessorInst,
            LevelMasterInst,
            _striker
        );

        if (actorAhead != null)
        {
            Object.FindAnyObjectByType<AudioMaster>().PlaySound("swing");
            _striker.StartCoroutine(new BeforeHitSwing(_striker, 0.5f).Execute());
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.Strike);
            yield return GlobalLowLevelConcrete.Pause;

            yield return actorAhead.RunBeforeDamageStatuses();
            yield return actorAhead.TakeBluntDamage(Value);
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
        return new StrikeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, Value, _striker);
    }
}