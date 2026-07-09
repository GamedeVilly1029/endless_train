using UnityEngine;

public class MoveOneTileBackwards : BaseAction
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            // new StepOneCellBackwardsConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, Actor)
            new StepXTilesBackwardConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, 1, Actor)
        };
    }
    public override BaseAction CreateClone(Transform transform)
    {
        MoveOneTileBackwards actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}