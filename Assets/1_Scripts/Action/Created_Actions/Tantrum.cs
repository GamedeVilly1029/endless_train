using UnityEngine;
using System.Collections.Generic;
using System;

public class Tantrum : BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
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
            HistoryBasedCondition.LastActionIsNotThisAction
        };

        BaseConstructElement removeVulnerable = new(this, conditions, ActionConcrete.RemoveTantrumVulnerability, ActionConcreteTag.Attack);
        ActionConstruct.Add(removeVulnerable);

        ValueConstructElement strike = new(this, conditions, ActionConcrete.StrikeConcrete, ActionConcreteTag.Attack, 2);
        ActionConstruct.Add(strike);

        BaseConstructElement add1Vulnerability = new(this, conditions, ActionConcrete.AddTantrumVulnerability, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);


        conditions = new()
        {
            HistoryBasedCondition.ConcreteHistoryIsEmpty
        };

        ValueConstructElement tantrumStrike = new(this, conditions, ActionConcrete.TantrumStrike, ActionConcreteTag.Attack, 2);
        ActionConstruct.Add(tantrumStrike);


        conditions = new()
        {
            HistoryBasedCondition.ConcreteHistoryHasOnly1Concrete
        };

        add1Vulnerability = new(this, conditions, ActionConcrete.AddTantrumVulnerability, ActionConcreteTag.Attack);
        ActionConstruct.Add(add1Vulnerability);
    }

    public override IAction CreateClone(Transform transform)
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