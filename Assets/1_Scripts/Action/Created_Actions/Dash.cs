using System;
using System.Collections.Generic;
using UnityEngine;

public class Dash: BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("DashActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("DashActionUI");
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
        ValueConstructElement charge = new(this, null, MovementConcrete.DashConcrete, ActionConcreteTag.Attack, 4);

        ActionConstruct.Add(charge);
    }

    public override IAction CreateClone(Transform transform)
    {
        Dash actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}