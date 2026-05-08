using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction: IAction
{
    public IAction PrototypeAction{get;set;} = null;
    public TurnProcessor TurnProcessorInstance{get;set;}
    public LevelMaster LevelMasterInstance{get;set;}
    public IActor Actor {get;set;}
    public GameObject UIRepresentation {get;set;}
    public List<IConcrete> ActionConstruct {get;set;}
    public List<IConcrete> TurnTemporarySuccessfulConcreteHistory {get;set;}
    public virtual int CooldownMax{get;set;}
    public virtual int Cooldown{get;set;}

    public void InitializeAction(IActor actor, TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        TurnProcessorInstance = turnProcessor;
        Actor = actor;
        LevelMasterInstance = levelMaster;
        InitializeChildAction();
    }

    public virtual void InitializeChildAction()
    {
        Debug.LogError("Base class placeholder is called - error"); 
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

    public List<IConcrete> CloneActionConstruct(IAction actionClone)
    {
        List<IConcrete> construct = new();
        foreach (IConcrete element in ActionConstruct)
        {
            IConcrete clonedElement = element.Clone(actionClone);
            construct.Add(clonedElement);
        }
        return construct;
    }

    public IEnumerator ExecuteAction()
    {
        TurnTemporarySuccessfulConcreteHistory = new();
        foreach (IConcrete element in ActionConstruct)
        {
            yield return element.Execute();
        }
        Actor.PositionCellIndexHistory.Push(Actor.PositionCellIndex);
        ActionManipulationMethods.RemoveFromActionRowAndShrinkIt(this);
        SetCooldown();
    }

    private void SetCooldown()
    {
        if (PrototypeAction.Actor is PlayerActor)
        {
            if (PrototypeAction != null)
            {
                PrototypeAction.Cooldown = PrototypeAction.CooldownMax;
            }
            else
            {
                Debug.LogError("Can't set cooldown, no action reference");
            }
        }
    }
}
