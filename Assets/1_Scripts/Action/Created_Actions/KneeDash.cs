using UnityEngine;
using System.Collections.Generic;
using System;

public class KneeDash : BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 1;
        Cooldown = 0;
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
        ActionConstruct = new();
        List<Func<DungeonMaster, IActor, bool>> conditions = new()
        {
            HistoryBasedCondition.PositionIndexChangedInPreviousAction
        };
        ValueConstructElement elem1 = new(this, conditions, ActionConcrete.StrikeConcrete, ActionConcreteTag.Attack, 10);
        ActionConstruct.Add(elem1);

        conditions = new()
        {
            HistoryBasedCondition.ConcreteHistoryIsEmpty
        };
        ValueConstructElement elem2 = new(this, conditions, ActionConcrete.StrikeConcrete, ActionConcreteTag.Attack, 5);
        ActionConstruct.Add(elem2);
    }

    public override IAction CreateClone(Transform transform)
    {
        KneeDash actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}