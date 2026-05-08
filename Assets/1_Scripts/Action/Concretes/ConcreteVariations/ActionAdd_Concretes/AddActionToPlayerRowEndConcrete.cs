using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActionToPlayerRowEndConcrete : ActionAddConcrete
{
    public AddActionToPlayerRowEndConcrete(TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag, IAction actionToAdd
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, actionToAdd)
    {
    }

    public override IEnumerator ChildExecute()
    {
        LevelMasterInst.Player.ActionRowInst.Actions.Add(ActionToAdd.CloneAndInstantiateUI(LevelMasterInst.Player.ActionRowInst.Panel, ActionToAdd));
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new AddActionToPlayerRowEndConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, ActionToAdd);
    }
}
