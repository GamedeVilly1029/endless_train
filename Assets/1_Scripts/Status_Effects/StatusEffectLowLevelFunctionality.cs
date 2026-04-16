using UnityEngine;
using System.Collections.Generic;

public static class StatusEffectLowLevelFunctionality
{
    public static List<IAction> ExtractCurrentActorActions(DungeonMaster dungeonMaster)
    {
        List<IAction> actions = new();
        foreach (IAction action in dungeonMaster.CurrentActor.ActionRowInst.Actions)
        {
            actions.Add(action);
        }
        return actions;
    }

    public static ValueConstructElement ReturnAttackConcrete(List<IAction> actions)
    {
        foreach (IAction action in actions)
        {
            foreach (ValueConstructElement element in action.ActionConstruct)
            {
                if (element.ConcreteTag == ActionConcreteTag.Attack)
                {
                    return element;
                }
            }
        }
        Debug.Log("None of provided actions contains Concrete with Attack tag");
        return null;
    }
}
