using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNextActionOfPlayerConcrete : ActionAddConcrete
{
    private IAction _actionOfThisConcreteRef;

    public ChangeNextActionOfPlayerConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    IAction actionToAdd
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, actionToAdd)
    {
        _actionOfThisConcreteRef = actionOfThisConcrete;
    }

    public override IEnumerator ChildExecute()
    {
        yield return GlobalLowLevelConcrete.Pause;

        IAction createdAction = ActionToAdd.CloneAndInstantiateUI(
            LevelMasterInst.Player.ActionRowInst.Panel,
            ActionToAdd
        );

        Transform createActionTransform =
            LevelMasterInst.Player.ActionRowInst.Panel.GetChild(
                LevelMasterInst.Player.ActionRowInst.Panel.childCount - 1
            );

        if (LevelMasterInst.Player.ActionRowInst.Actions.Count > 0)
        {
            ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(
                LevelMasterInst.Player.ActionRowInst.Actions[0]
            );
        }

        LevelMasterInst.Player.ActionRowInst.Actions.Insert(0, createdAction);

        createActionTransform.SetSiblingIndex(
            _actionOfThisConcreteRef.UIRepresentation.transform.GetSiblingIndex() + 1 // Looks sus, probably won't work as expected
        );

        yield return GlobalLowLevelConcrete.Pause;
    }
        public override IConcrete Clone(IAction clonedAction)
    {
        return new ChangeNextActionOfPlayerConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, ActionToAdd);
    }
}