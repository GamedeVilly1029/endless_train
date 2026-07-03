using System.Collections;
using UnityEngine;

public class StunFirstPlayerActionStatusConcrete : ActionAddStatusConcrete
{
    private BaseAction _stunAction;
    public StunFirstPlayerActionStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseActor actor, 
        BaseAction actionToAssign
    ):base(turnProcessor, levelMaster, actor, actionToAssign)
    {
        _stunAction = actionToAssign;
    }

    public override IEnumerator ChildExecute()
    {
        if (LevelMasterInst.Player.ActionRowInst.Actions.Count > 0)
        {
            ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(LevelMasterInst.Player.ActionRowInst.Actions[0]);
        }

        BaseAction createdAction = _stunAction.CloneAndInstantiateUI(LevelMasterInst.Player.ActionRowInst.Panel, ActionToAssign);
        createdAction.UIRepresentation.transform.SetSiblingIndex(0);
        LevelMasterInst.Player.ActionRowInst.Actions.Insert(0, createdAction);
        yield return null;
    }
}
