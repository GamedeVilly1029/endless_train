using UnityEngine;
using System;
using System.Collections;

public static class StaticActionFunctionality
{
    public static Action CreateActionWithUI(Func<DungeonMaster, IEnumerator> concrete, GameObject UIRepresentationPrefab, int valueForActionConcrete)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.Concrete = concrete;
        action.ActionConstruct.Add(constructElement);

        action.ValueForActionConcrete = valueForActionConcrete;

        action.UIRepresentation = UIRepresentationPrefab;
        return action;
    }

    public static Action CreateActionWithoutUI(Func<DungeonMaster, IEnumerator> concrete, int valueForActionConcrete)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.Concrete = concrete;
        action.ActionConstruct.Add(constructElement);

        action.ValueForActionConcrete = valueForActionConcrete;

        return action;
    }
}
