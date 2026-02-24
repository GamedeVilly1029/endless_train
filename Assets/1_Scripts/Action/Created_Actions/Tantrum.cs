using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Tantrum : IAction
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
        if (Resources.Load<GameObject>("TantrumActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("TantrumActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new();
        List<Func<DungeonMaster, IActor, bool>> conditions = new()
        {
            ActionConditions.LastActionIsNotThisAction
        };

        ActionConstructElement removeVulnerable = new(this, conditions, ActionConcretes.RemoveTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(removeVulnerable);

        ActionConstructElement strike = new(this, conditions, ActionConcretes.AttackEntityAhead, 2, ActionConcreteTag.Attack);
        ActionConstruct.Add(strike);

        ActionConstructElement add1Vulnerability = new(this, conditions, ActionConcretes.AddTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);


        conditions = new()
        {
            ActionConditions.ConcreteHistoryIsEmpty
        };

        ActionConstructElement tantrumStrike = new(this, conditions, ActionConcretes.TantrumStrike, 2, ActionConcreteTag.Attack);
        ActionConstruct.Add(tantrumStrike);


        conditions = new()
        {
            ActionConditions.ConcreteHistoryHasOnly1Concrete
        };

        add1Vulnerability = new(this, conditions, ActionConcretes.AddTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);
    }

    public IAction CloneAndInstantiateUI(Transform transform)
    {
        Tantrum actionClone = new()
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
