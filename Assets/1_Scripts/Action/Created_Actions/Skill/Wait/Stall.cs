using UnityEngine;

public class Stall : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StallConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Stall actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}