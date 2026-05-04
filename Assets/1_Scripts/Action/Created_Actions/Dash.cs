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
        ActionConstruct = new()
        {
            new DashConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Move, 4, Actor)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        Dash actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}