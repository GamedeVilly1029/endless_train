using UnityEngine;

public class StrikeBackwards : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StrikeBackwardsConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 7, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        StrikeBackwards actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}