using System.Collections;
using System.Linq;
using UnityEngine;

public static class ActionAdditionConcrete
{
    private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static IEnumerator AddActionToPlayerRowEnd(IConstructElement element)
    // {
    //     ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
    //     _levelMaster.Player.ActionRowInst.Actions.Add(casted.ActionToAssign.CloneAndInstantiateUI(_levelMaster.Player.ActionRowInst.Panel, casted.ActionToAssign));
    //     yield return GlobalLowLevelConcrete.Pause;
    // }

    // public static IEnumerator AddActionToPlayerRowStart(IConstructElement element)
    // {
    //     ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
    //     IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(_levelMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
    //     Transform createActionTransform = _levelMaster.Player.ActionRowInst.Panel.GetChild(_levelMaster.Player.ActionRowInst.Panel.childCount - 1);

    //     _levelMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);
    //     createActionTransform.SetSiblingIndex(element.ActionOfThisConcrete.UIRepresentation.transform.GetSiblingIndex() + 1);
    //     yield return GlobalLowLevelConcrete.Pause;
    // }

    // public static IEnumerator ChangeNextActionOfPlayer(IConstructElement element)
    // {
    //     yield return GlobalLowLevelConcrete.Pause;
    //     yield return GlobalLowLevelConcrete.Pause;


    //     ActionAssignmentConstructElement casted = element as ActionAssignmentConstructElement;
    //     IAction createdAction = casted.ActionToAssign.CloneAndInstantiateUI(_levelMaster.Player.ActionRowInst.Panel, casted.ActionToAssign);
    //     Transform createActionTransform = _levelMaster.Player.ActionRowInst.Panel.GetChild(_levelMaster.Player.ActionRowInst.Panel.childCount - 1);

    //     if (_levelMaster.Player.ActionRowInst.Actions.Count > 0)
    //     {
    //         ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(_levelMaster.Player.ActionRowInst.Actions[0]);
    //     }

    //     _levelMaster.Player.ActionRowInst.Actions.Insert(0, createdAction);
    //     createActionTransform.SetSiblingIndex(element.ActionOfThisConcrete.UIRepresentation.transform.GetSiblingIndex() + 1);

    //     yield return GlobalLowLevelConcrete.Pause;
    // }
}