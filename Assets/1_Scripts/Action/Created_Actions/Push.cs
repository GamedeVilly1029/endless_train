using UnityEngine;
public class Push : BaseAction
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new PushConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Push, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Push actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}