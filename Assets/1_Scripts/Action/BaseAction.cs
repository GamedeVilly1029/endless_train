using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction
{
    public BaseAction PrototypeAction = null;
    [HideInInspector] public TurnProcessor TurnProcessorInst;
    [HideInInspector] public LevelMaster LevelMasterInst;
    [HideInInspector] public BaseActor Actor; 
    [HideInInspector] public GameObject UIRepresentation; 
    public List<IConcrete> ActionConstruct; 
    public List<IConcrete> TurnTemporarySuccessfulConcreteHistory; 
    [HideInInspector] public int CooldownMax;
    [HideInInspector] public int Cooldown;

    public void Initialize
    (
        BaseActor actor, 
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        int maxCooldown,
        string pathToUI
    )
    {
        TurnProcessorInst = turnProcessor;
        Actor = actor;
        LevelMasterInst = levelMaster;
        CooldownMax = maxCooldown;
        Cooldown = 0;

        if (Resources.Load<GameObject>(pathToUI) != null)
        {
            UIRepresentation = Resources.Load<GameObject>(pathToUI);
        }
        else
        {
            Debug.LogError($"Resources.Load can't find UIRepresentationAsset or {pathToUI} contains typos");
        }

        InitializeChild();
        InitializeConstruct();
    }

    public virtual void InitializeChild()
    {
        return;
    }

    public virtual void InitializeConstruct()
    {
        return;
    }

    public BaseAction CloneAndInstantiateUI(Transform transform, BaseAction prototypeAction)
    {
        BaseAction clone = CreateClone(transform);
        SetReferenceAction(prototypeAction, clone);
        if (transform == Actor.ActionRowInst.Panel) // Automatically adds UI place for the ET_Action.
        {
            Actor.ActionRowInst.OnActionAdd.Invoke();
        }

        return clone;
    }

    public virtual BaseAction CreateClone(Transform transform)
    {
        Debug.Log("Base class placeholder is called - error");
        return null;
    }
    
    public void SetReferenceAction(BaseAction prototype, BaseAction actionClone)
    {
        actionClone.PrototypeAction = prototype;
    }

    public List<IConcrete> CloneActionConstruct(BaseAction actionClone)
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
        Actor.TurnBasedActionHistory.Add(this);
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