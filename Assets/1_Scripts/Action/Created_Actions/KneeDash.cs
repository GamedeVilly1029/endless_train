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
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInstance,
            LevelMasterInstance,
            this,
            new List<IConditionCommand>()
            {
                new PositionIndexChangedInPreviousActionCondition(TurnProcessorInstance, LevelMasterInstance, Actor)
            },
            ActionConcreteTag.Attack,
            5
            ),

            new StrikeConcrete(TurnProcessorInstance,
            LevelMasterInstance,
            this,
            new List<IConditionCommand>()
            {
                new ConcreteHistoryIsEmptyCondition(TurnProcessorInstance, LevelMasterInstance)
            },
            ActionConcreteTag.Attack,
            5
            )
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        KneeDash actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}