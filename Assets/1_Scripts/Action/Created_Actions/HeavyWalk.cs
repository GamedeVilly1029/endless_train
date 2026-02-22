using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class HeavyWalk : IAction
{
    public DungeonMaster DungeonMasterInstance{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public List<ActionConstructElement> ActionConstruct {get;set;}
    public List<System.Func<DungeonMaster, ActionConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory {get;set;}

    public void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("HeavyWalkActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("HeavyWalkActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        // Here, what to do.
        ActionConstruct = new();
        ActionConstructElement elem1 = new(this, null, ActionConcretes.AttackEntityAhead, 4, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem1);

        ActionConstructElement elem2 = new(this, null, ActionConcretes.Push, 0, ActionConcreteTag.Push);
        ActionConstruct.Add(elem2);
        
        ActionConstructElement elem3 = new(this, null, ActionConcretes.MoveOneCellForward, 0, ActionConcreteTag.Move);
        ActionConstruct.Add(elem3);
    }

    public IAction CloneAndInstantiateUI(Transform transform)
    {
        Strike actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }

    public List<ActionConstructElement> CloneActionConstruct(IAction actionClone)
    {
        List<ActionConstructElement> concretes = new();
        foreach (var concrete in ActionConstruct)
        {
            var newElement = new ActionConstructElement(
                actionClone, 
                concrete.ConditionsToExecuteConcrete, 
                concrete.Concrete, 
                concrete.ConcreteValue,
                concrete.ConcreteTag);
            concretes.Add(newElement);
        }
        return concretes;
    }

    
    public IEnumerator ExecuteAction(DungeonMaster dungeonMaster)
    {
        TurnTemporarySuccessfulConcreteHistory = new();
        foreach (ActionConstructElement element in ActionConstruct)
        {
            yield return element.ExecuteConcrete(DungeonMasterInstance);
        }
        if (UIRepresentation != null)
        {
            UnityEngine.Object.Destroy(UIRepresentation);
            UIRepresentation = null;
        }
        Actor.PositionCellIndexHistory.Push(Actor.PositionCellIndex);
    }
}