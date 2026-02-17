using System.Collections.Generic;
using UnityEngine;

public class Strike : IAction
{
    public DungeonMaster DungeonMaster{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public int ValueForActionConcrete {get;set;}
    public List<ActionConstructElement> ActionConstruct {get;set;}



    public void InitializeAction(IActor actor, int valueForActionConcrete, DungeonMaster dungeonMaster)
    {
        DungeonMaster = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("AttackActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("AttackActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        ValueForActionConcrete = valueForActionConcrete;
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ActionConstructElement()
            {
                ConditionsToExecuteConcrete = null,
                Concrete = ActionConcretes.AttackEntityAhead
            }
        };
    }

    public IAction CloneAndInstantiateUI(Transform transform)
    {
        Strike actionClone = new()
        {
            DungeonMaster = DungeonMaster,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
            ValueForActionConcrete = ValueForActionConcrete,
            ActionConstruct = CloneActionConstruct()
        };

        return actionClone;
    }

    public List<ActionConstructElement> CloneActionConstruct()
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
