using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAddConcrete : BaseConcrete
{
    public IAction ActionToAdd;
    public ActionAddConcrete(
    TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    IAction actionToAdd
    ): base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        ActionToAdd = actionToAdd;
    }

    public override IEnumerator ChildExecute()
    {
        Debug.LogWarning("ActionAddConcrete's base Execute method version was called. Implement proper concrete");
        yield break;
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        ActionAddConcrete clone = new(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, ActionToAdd);
        return clone;
    }
}
