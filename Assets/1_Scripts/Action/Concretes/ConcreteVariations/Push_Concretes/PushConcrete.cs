using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushConcrete : BaseConcrete
{
    public BaseActor _pusher;
    public BaseActor _actorAhead;
    public PushConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    BaseAction actionOfThisConcrete,
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

        Object.FindAnyObjectByType<AudioMaster>().PlaySound("push");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override List<IConditionCommand> CreateBaseConditionList()
    {
        List<IConditionCommand> conds = new();

        _actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(TurnProcessorInst, LevelMasterInst, _pusher);
        if (_actorAhead != null)
        {
            conds.Add(new IsPushAbleCondition(TurnProcessorInst, LevelMasterInst, _actorAhead));
        }

        return conds;
    }

    public override IEnumerator DeclinedConcrete()
    {
        ActorParticlePlayer.PlayParticles(_pusher, ParticleType.Push);
        Object.FindAnyObjectByType<AudioMaster>().PlaySound("push");
        yield break;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new PushConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _pusher);
    }
}