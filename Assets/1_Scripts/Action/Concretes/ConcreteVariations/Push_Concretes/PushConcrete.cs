using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushConcrete : BaseConcrete
{
    BaseActor _pusher;
    BaseActor _actorAhead;
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
        // ExtraConditions.Add(new IsPushAbleCondition(TurnProcessorInst, LevelMasterInst, _actorAhead)); // This is not triggered, because _actorAhead assigned after this condition runs.
    }

    public override IEnumerator ChildExecute()
    {
        ActorParticlePlayer.PlayParticles(_pusher, ParticleType.Push);

        if (new CellAheadExistsCondition(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor).Execute())
        {
            _actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor);
            if (_actorAhead != null)
            {
                if (new AdjacentCellsExistsCondition(TurnProcessorInst, LevelMasterInst, _actorAhead).Execute())
                {
                    yield return new BePushedConcrete(
                        TurnProcessorInst,
                        LevelMasterInst,
                        ActionOfThisConcrete,
                        null,
                        ActionConcreteTag.Move,
                        _actorAhead,
                        TurnProcessorInst.CurrentActor.IsFacingRight()
                    ).Execute();
                }
            }
        }

        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IEnumerator DeclinedConcrete()
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