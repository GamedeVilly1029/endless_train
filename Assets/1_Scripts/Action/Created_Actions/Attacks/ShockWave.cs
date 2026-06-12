using UnityEngine;

public class ShockWave : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ShockWaveConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        ShockWave actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}