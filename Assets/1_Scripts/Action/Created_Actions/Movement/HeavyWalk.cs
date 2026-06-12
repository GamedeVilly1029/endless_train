using UnityEngine;

public class HeavyWalk : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor),
            new PushConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Push, Actor),
            new StepXTilesForwardConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, 1, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        HeavyWalk actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}