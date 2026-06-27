using UnityEngine;

public class TeleportBehindActorAhead : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new TeleportBehindActorAheadConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        TeleportBehindActorAhead actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}