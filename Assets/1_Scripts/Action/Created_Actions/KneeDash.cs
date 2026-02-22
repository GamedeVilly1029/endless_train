using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class KneeDash : IAction
{
    public DungeonMaster DungeonMasterInstance{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public List<ActionConstructElement> ActionConstruct {get;set;}
    public List<Func<DungeonMaster, ActionConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory {get;set;}

    public void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("KneeDashActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("KneeDashActionUI");
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
        List<Func<DungeonMaster, IActor, bool>> conditions = new()
        {
            ActionConditions.PositionIndexChangedInPreviousAction
        };
        ActionConstructElement elem1 = new(this, conditions, ActionConcretes.AttackEntityAhead, 10, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem1);

        conditions = new()
        {
            ActionConditions.ConcreteHistoryIsEmpty
        };
        ActionConstructElement elem2 = new(this, conditions, ActionConcretes.AttackEntityAhead, 5, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem2);
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