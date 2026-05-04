using UnityEngine;
using System.Collections.Generic;

public static class StatusEffectLowLevelFunctionality
{
    public static List<IAction> ExtractActorActions(IActor actor)
    {
        List<IAction> actions = new();
        foreach (IAction action in actor.ActionRowInst.Actions)
        {
            actions.Add(action);
        }
        return actions;
    }

    public static ValueConcrete ReturnAttackConcrete(List<IAction> actions)
    {
        foreach (IAction action in actions)
        {
            foreach (BaseConcrete element in action.ActionConstruct)
            {
                if (element is ValueConcrete && element.Tag == ActionConcreteTag.Attack)
                {
                    return element as ValueConcrete;
                }
            }
        }
        Debug.Log("None of provided actions contains Concrete with Attack tag");
        return null;
    }
}
