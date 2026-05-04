using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOneCellBackwardsConcrete : BaseConcrete
{
    private IActor _actorToMove;
    public StepOneCellBackwardsConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    IActor actor
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actorToMove = actor;
    }

    public override IEnumerator ChildExecute()
    {
        IConditionCommand cellBehindExists = new CellBehindExistsCondition(TurnProcessorInst, LevelMasterInst, _actorToMove);
        IConditionCommand cellBehindIsEmpty = new CellBehindIsEmptyCondition(TurnProcessorInst, LevelMasterInst, _actorToMove);
        if (!cellBehindExists.Execute())
        {
            yield break;
        }
        else if (!cellBehindIsEmpty.Execute())
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return MovementLowLevelConcrete.StepForwardOrBackwards(_actorToMove, Resources.Load<MoveData>("StepData"), false);
        }
        yield return GlobalLowLevelConcrete.Pause;
    }
}