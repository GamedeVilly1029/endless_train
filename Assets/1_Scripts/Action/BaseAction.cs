using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction: IAction
{
    public IAction PrototypeAction{get;set;} = null;
    public DungeonMaster DungeonMasterInstance{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public List<IConstructElement> ActionConstruct {get;set;}
    public List<System.Func<DungeonMaster, IConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory {get;set;}
    public virtual int CooldownMax{get;set;}
    public virtual int Cooldown{get;set;}

    public void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        InitializeBaseAction(actor, dungeonMaster);
        InitializeChildAction();
    }

    public virtual void InitializeChildAction()
    {
        Debug.LogError("Base class placeholder is called - error"); 
    }

    private void InitializeBaseAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
    }

    public IAction CloneAndInstantiateUI(Transform transform, IAction prototypeAction)
    {
        IAction clone = CreateClone(transform);
        SetReferenceAction(prototypeAction, clone);
        if (transform == Actor.ActionRowInst.Panel) // Automatically adds UI place for the ET_Action.
        {
            Actor.ActionRowInst.OnActionAdd.Invoke();
        }

        return clone;
    }

    public virtual IAction CreateClone(Transform transform)
    {
        Debug.Log("Base class placeholder is called - error");
        return null;
    }
    
    public void SetReferenceAction(IAction prototype, IAction actionClone)
    {
        actionClone.PrototypeAction = prototype;
    }

    public List<IConstructElement> CloneActionConstruct(IAction actionClone)
    {
        List<IConstructElement> construct = new();
        foreach (var element in ActionConstruct)
        {
            IConstructElement clonedElement = element.Clone(actionClone);
            construct.Add(clonedElement);
        }
        return construct;
    }

    public IEnumerator ExecuteAction(DungeonMaster dungeonMaster)
    {
        TurnTemporarySuccessfulConcreteHistory = new();
        foreach (IConstructElement element in ActionConstruct)
        {
            yield return element.Execute(DungeonMasterInstance);
        }
        Actor.PositionCellIndexHistory.Push(Actor.PositionCellIndex);
        ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(this);
        SetCooldown(PrototypeAction.Actor);
    }

    private void SetCooldown(IActor actor)
    {
        if (actor is PlayerActor)
        {
            if (PrototypeAction != null)
            {
                PrototypeAction.Cooldown = PrototypeAction.CooldownMax;
            }
            else
            {
                Debug.Log("Can't set cooldown, no action reference");
            }
        }
    }
}
