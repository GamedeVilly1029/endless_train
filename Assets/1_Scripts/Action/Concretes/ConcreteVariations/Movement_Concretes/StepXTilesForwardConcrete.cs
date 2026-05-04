using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepXTilesForwardConcrete : ValueConcrete
{
    IActor _actorToMove;
    public StepXTilesForwardConcrete(TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    int value,
    IActor actorToMove
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _actorToMove = actorToMove;
    }

    public override IEnumerator ChildExecute()
    {
        int stepsToSubtract = Value;
        while (stepsToSubtract > 0)
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return new StepOneCellForwardConcrete(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, ActionConcreteTag.Move, _actorToMove);

            stepsToSubtract--;
        }
    }
}
