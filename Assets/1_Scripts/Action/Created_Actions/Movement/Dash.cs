using System;
using System.Collections.Generic;
using UnityEngine;

public class Dash: BaseAction
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new DashConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, 4, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Dash actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}