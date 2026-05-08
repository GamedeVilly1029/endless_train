using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushConcrete : BaseConcrete
{
    public PushConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        ParticlePlayer.StartPush(TurnProcessorInst.CurrentActor);

        if (new CellAheadExistsCondition(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor).Execute())
        {
            IActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor);
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
        ParticlePlayer.StopPush(TurnProcessorInst.CurrentActor);
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new PushConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag);
    }
}