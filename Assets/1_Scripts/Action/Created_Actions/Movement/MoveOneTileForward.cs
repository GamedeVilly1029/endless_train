using UnityEngine;

public class MoveOneTileForward : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StepXTilesForwardConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, 1, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        MoveOneTileForward actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}