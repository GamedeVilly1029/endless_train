using UnityEngine;

public class Explode : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ExplodeConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, 10, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Explode actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}