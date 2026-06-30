using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeBackwardsConcrete: ValueConcrete
{
    BaseActor _striker;
    public StrikeBackwardsConcrete(
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
        BaseActor actorBehind = GlobalLowLevelConcrete.TryReturnActorBehind(
            TurnProcessorInst,
            LevelMasterInst,
            _striker
        );

        if (actorBehind != null)
        {
            yield return actorBehind.TakeBluntDamage(Value);
            yield return actorBehind.RunBeforeDamageStatuses();

            Object.FindAnyObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.KickBack);
            yield return GlobalLowLevelConcrete.Pause;
            Object.FindAnyObjectByType<AudioMaster>().PlaySound("hit");
        }
        else
        {
            Object.FindAnyObjectByType<AudioMaster>().PlaySound("swing");
            ActorParticlePlayer.PlayParticles(_striker, ParticleType.KickBack);
            yield return GlobalLowLevelConcrete.Pause;
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new StrikeBackwardsConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _striker);
    }
}