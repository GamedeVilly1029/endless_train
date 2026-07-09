using UnityEngine;

public class Strike : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Strike actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}