using UnityEngine;

public class KickPush : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor),
            new PushConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        KickPush actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}