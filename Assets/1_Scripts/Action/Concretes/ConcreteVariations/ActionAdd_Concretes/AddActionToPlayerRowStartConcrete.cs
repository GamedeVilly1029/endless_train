using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActionToPlayerRowStartConcrete : ActionAddConcrete
{
    private BaseAction _actionOfThisConcreteRef;

    public AddActionToPlayerRowStartConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    BaseAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    BaseAction actionToAdd
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, actionToAdd)
    {
        _actionOfThisConcreteRef = actionOfThisConcrete;
    }

    public override IEnumerator ChildExecute()
    {
        BaseAction createdAction = ActionToAdd.CloneAndInstantiateUI(
            LevelMasterInst.Player.ActionRowInst.Panel,
            ActionToAdd
        );

        Transform createActionTransform =
            LevelMasterInst.Player.ActionRowInst.Panel.GetChild(
                LevelMasterInst.Player.ActionRowInst.Panel.childCount - 1
            );

        LevelMasterInst.Player.ActionRowInst.Actions.Insert(0, createdAction);

        createActionTransform.SetSiblingIndex(
            _actionOfThisConcreteRef.UIRepresentation.transform.GetSiblingIndex() + 1
        );

        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new AddActionToPlayerRowStartConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, ActionToAdd);
    }
}