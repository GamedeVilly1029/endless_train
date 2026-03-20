using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Tantrum : BaseAction
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
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

        ActionConstructElement removeVulnerable = new(this, conditions, ActionConcrete.RemoveTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(removeVulnerable);

        ActionConstructElement strike = new(this, conditions, ActionConcrete.StrikeConcrete, 2, ActionConcreteTag.Attack);
        ActionConstruct.Add(strike);

        ActionConstructElement add1Vulnerability = new(this, conditions, ActionConcrete.AddTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);


        conditions = new()
        {
            ActionConditions.ConcreteHistoryIsEmpty
        };

        ActionConstructElement tantrumStrike = new(this, conditions, ActionConcrete.TantrumStrike, 2, ActionConcreteTag.Attack);
        ActionConstruct.Add(tantrumStrike);


        conditions = new()
        {
            ActionConditions.ConcreteHistoryHasOnly1Concrete
        };

        add1Vulnerability = new(this, conditions, ActionConcrete.AddTantrumVulnerability, 0, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
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
}