using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushConcrete : BaseConcrete
{
    BaseActor _pusher;
    public PushConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    BaseActor pusher
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _pusher = pusher;
    }

    public override IEnumerator ChildExecute()
    {
        ActorParticlePlayer.PlayParticles(_pusher, ParticleType.Push);

        if (new CellAheadExistsCondition(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor).Execute())
        {
            BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor);
            if (actorAhead != null)
            {
                if (new AdjacentCellsExistsCondition(TurnProcessorInst, LevelMasterInst, actorAhead).Execute())
                {
                    yield return new BePushedConcrete(
                        TurnProcessorInst,
                        LevelMasterInst,
                        ActionOfThisConcrete,
                        null,
                        ActionConcreteTag.Move,
                        actorAhead,
                        TurnProcessorInst.CurrentActor.IsFacingRight()
                    ).Execute();
                }
            }
        }

        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IEnumerator FalseConditionConcrete()
    {
        ActorParticlePlayer.PlayParticles(_pusher, ParticleType.Push);
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield break;
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new PushConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _pusher);
    }
}