using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Rotate : IAction
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
        if (Resources.Load<GameObject>("RotateActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("RotateActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ActionConstructElement(this, null, ActionConcretes.RotateActor, 0, ActionConcreteTag.Move)
        };
    }

    public IAction CloneAndInstantiateUI(Transform transform)
    {
        Strike actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
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
            Object.Destroy(UIRepresentation);
            UIRepresentation = null;
        }
        Actor.PositionCellIndexHistory.Push(Actor.PositionCellIndex);
    }
}
