using UnityEngine;
using System.Collections.Generic;
using System;

public class KneeDash : BaseAction
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInst,
            LevelMasterInst,
            this,
            new List<IConditionCommand>()
            {
                new PositionIndexChangedInPreviousActionCondition(TurnProcessorInst, LevelMasterInst, Actor)
            },
            ActionConcreteTag.Attack,
            5,
            Actor
            ),

            new StrikeConcrete(TurnProcessorInst,
            LevelMasterInst,
            this,
            new List<IConditionCommand>()
            {
                new ConcreteHistoryIsEmptyCondition(TurnProcessorInst, LevelMasterInst)
            },
            ActionConcreteTag.Attack,
            5,
            Actor
            )
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        KneeDash actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}