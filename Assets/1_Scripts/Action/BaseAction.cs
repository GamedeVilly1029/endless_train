using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction: IAction
{
    public DungeonMaster DungeonMasterInstance{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public List<ActionConstructElement> ActionConstruct {get;set;}
    public List<System.Func<DungeonMaster, ActionConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory {get;set;}

    public virtual void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        Debug.Log("Base class placeholder is called - error"); 
    }

    public virtual IAction CloneAndInstantiateUI(Transform transform)
    {
        Debug.Log("Base class placeholder is called - error");
        return null;
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
        Actor.ActionRowInst.OnActionRemove.Invoke();
    }
}
