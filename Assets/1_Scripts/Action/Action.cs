using UnityEngine;
using System.Collections.Generic;

public class Action
{
    public IActor Actor;
    public GameObject UIRepresentation;
    public int ValueForActionConcrete;
    public List<ActionConstructElement> ActionConstruct;

    public Action CloneAndInstantiateUI(Transform transformForUI)
    {
        Action actionClone = new()
        {
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transformForUI),
            ValueForActionConcrete = ValueForActionConcrete,
            ActionConstruct = CloneActionConstruct()
        };

        return actionClone;
    }

    public Action CloneWithoutUI()
    {
        Action actionClone = new()
        {
            Actor = Actor,
            UIRepresentation = null,
            ValueForActionConcrete = ValueForActionConcrete,
            ActionConstruct = CloneActionConstruct()
        };

        return actionClone;
    }

    private List<ActionConstructElement> CloneActionConstruct()
    {
        List<ActionConstructElement> concretes = new();
        foreach (var concrete in ActionConstruct)
        {
            var newElement = new ActionConstructElement
            {
                ConditionsToExecuteConcrete = concrete.ConditionsToExecuteConcrete,
                Concrete = concrete.Concrete
            };
            concretes.Add(concrete);
        }
        return concretes;
    }
}