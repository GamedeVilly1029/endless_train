using UnityEngine;
using System.Collections.Generic;

public class Action
{
    public IActor Actor;
    public GameObject UIRepresentation;
    public GameObject PrefabOfAttack; // Think about it
    public int ValueForActionConcrete;
    public List<ActionConstructElement> ActionConstruct;

    public Action CloneAndInstantiateUI(Transform transformForUI)
    {
        Action actionClone = new();
        actionClone.Actor = Actor;
        actionClone.UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transformForUI);
        actionClone.ValueForActionConcrete = ValueForActionConcrete;
        actionClone.ActionConstruct = CloneActionConstruct();

        return actionClone;
    }

    public Action CloneWithoutUI()
    {
        Action actionClone = new();
        actionClone.Actor = Actor;
        actionClone.UIRepresentation = null;
        actionClone.ValueForActionConcrete = ValueForActionConcrete;
        actionClone.ActionConstruct = CloneActionConstruct();

        return actionClone;
    }

    private List<ActionConstructElement> CloneActionConstruct()
    {
        List<ActionConstructElement> concretes = new();
        foreach (var concrete in ActionConstruct)
        {
            var newElement = new ActionConstructElement();
            newElement.ConditionsToExecuteConcrete = concrete.ConditionsToExecuteConcrete;
            newElement.Concrete = concrete.Concrete;
            concretes.Add(concrete);
        }
        return concretes;
    }
}