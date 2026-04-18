using System.Collections;
using System.Linq;
using UnityEngine;

public static class ActionAdditionConcrete
{
    public static IEnumerator AddActionToPlayerRowEnd(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
        dungeonMaster.Player.ActionRowInst.Actions.Add(casted.ActionToAssign.CloneAndInstantiateUI(dungeonMaster.Player.ActionRowInst.Panel, casted.ActionToAssign));
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator AddActionToPlayerRowStart(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
        IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(dungeonMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
        Transform createActionTransform = dungeonMaster.Player.ActionRowInst.Panel.GetChild(dungeonMaster.Player.ActionRowInst.Panel.childCount - 1);

        dungeonMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);
        createActionTransform.SetSiblingIndex(element.ActionOfThisConcrete.UIRepresentation.transform.GetSiblingIndex() + 1);
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator ChangeNextActionOfPlayer(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return LowLevelConcrete.Pause;
        yield return LowLevelConcrete.Pause;


        ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
        IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(dungeonMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
        Transform createActionTransform = dungeonMaster.Player.ActionRowInst.Panel.GetChild(dungeonMaster.Player.ActionRowInst.Panel.childCount - 1);

        if (dungeonMaster.Player.ActionRowInst.Actions.Count > 0)
        {
            ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(dungeonMaster.Player.ActionRowInst.Actions[0]);
        }

        dungeonMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);
        createActionTransform.SetSiblingIndex(element.ActionOfThisConcrete.UIRepresentation.transform.GetSiblingIndex() + 1);

        yield return LowLevelConcrete.Pause;
    }
}