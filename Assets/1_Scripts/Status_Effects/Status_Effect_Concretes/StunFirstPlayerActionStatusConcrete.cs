using System.Collections;
using UnityEngine;

public class StunFirstPlayerActionStatusConcrete : ActionAddStatusConcrete
{
    private IAction _stunAction;
    public StunFirstPlayerActionStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        IActor actor, 
        IAction actionToAssign
    ):base(turnProcessor, levelMaster, actor, actionToAssign)
    {
        _stunAction = actionToAssign;
    }

    public override IEnumerator ChildExecute()
    {
        yield return GlobalLowLevelConcrete.Pause;

        if (LevelMasterInst.Player.ActionRowInst.Actions.Count > 0)
        {
            ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(LevelMasterInst.Player.ActionRowInst.Actions[0]);
        }

        IAction createdAction = _stunAction.CloneAndInstantiateUI(LevelMasterInst.Player.ActionRowInst.Panel, ActionToAssign);
        createdAction.UIRepresentation.transform.SetSiblingIndex(0);
        LevelMasterInst.Player.ActionRowInst.Actions.Insert(0, createdAction);

        yield return GlobalLowLevelConcrete.Pause;
    }
}
