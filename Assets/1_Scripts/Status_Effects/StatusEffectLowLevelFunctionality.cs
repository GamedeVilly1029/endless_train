using UnityEngine;
using System.Collections.Generic;

public static class StatusEffectLowLevelFunctionality
{
    public static List<BaseAction> ExtractActorActions(BaseActor actor)
    {
        List<BaseAction> actions = new();
        foreach (BaseAction action in actor.ActionRowInst.Actions)
        {
            actions.Add(action);
        }
        return actions;
    }

    public static ValueConcrete ReturnAttackConcrete(List<BaseAction> actions)
    {
        foreach (BaseAction action in actions)
        {
            foreach (BaseConcrete element in action.ActionConstruct)
            {
                if (element is ValueConcrete && element.Tag == ActionConcreteTag.Attack)
                {
                    return element as ValueConcrete;
                }
            }
        }
        return null;
    }
}
