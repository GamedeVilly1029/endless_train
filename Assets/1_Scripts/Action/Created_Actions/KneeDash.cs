using UnityEngine;
using System.Collections.Generic;
using System;

public class KneeDash : BaseAction
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
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
        ActionConstruct = new();
        List<Func<DungeonMaster, IActor, bool>> conditions = new()
        {
            ActionConditions.PositionIndexChangedInPreviousAction
        };
        ActionConstructElement elem1 = new(this, conditions, ActionConcrete.StrikeConcrete, 10, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem1);

        conditions = new()
        {
            ActionConditions.ConcreteHistoryIsEmpty
        };
        ActionConstructElement elem2 = new(this, conditions, ActionConcrete.StrikeConcrete, 5, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem2);
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
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