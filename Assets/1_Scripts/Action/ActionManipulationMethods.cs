using UnityEngine;

public static class ActionManipulationMethods
{
    public static void RemoveFromActionRowAndShrinkIt(IAction action)
    {
        if (action.UIRepresentation != null)
        {
            Object.Destroy(action.UIRepresentation);
            action.UIRepresentation = null;
        }
        action.Actor.ActionRowInst.Actions.Remove(action);
        action.Actor.ActionRowInst.OnActionRemove.Invoke();
    }

    public static void RemoveFromActionRow(IAction action)
    {
        if (action.UIRepresentation != null)
        {
            Object.Destroy(action.UIRepresentation);
            action.UIRepresentation = null;
        }
        action.Actor.ActionRowInst.Actions.Remove(action);
    }
}