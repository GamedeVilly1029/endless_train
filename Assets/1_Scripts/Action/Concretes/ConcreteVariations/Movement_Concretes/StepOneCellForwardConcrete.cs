using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOneCellForwardConcrete : BaseConcrete
{
    private BaseActor _actorToMove;

    public StepOneCellForwardConcrete
    (
    TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    BaseAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    BaseActor actorToMove
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actorToMove = actorToMove;
    }

    public override IEnumerator ChildExecute()
    {
        IConditionCommand cellAheadExists = new CellAheadExistsCondition(TurnProcessorInst, LevelMasterInst, _actorToMove);
        IConditionCommand cellAheadIsEmpty = new CellAheadIsEmptyCondition(TurnProcessorInst, LevelMasterInst, _actorToMove);
        if (!cellAheadExists.Execute())
        {
            yield break;
        }
        else if (!cellAheadIsEmpty.Execute())
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return MovementLowLevelConcrete.StepForwardOrBackwards(LevelMasterInst, _actorToMove, Resources.Load<MoveData>("StepData"), true);
            Debug.Log("StepOneCellForwardConcrete - stepped forward");
        }
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new StepOneCellForwardConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _actorToMove);
    }
}