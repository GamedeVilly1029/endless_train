using UnityEngine;

public class TeleportBehind : BaseAction 
{
    private BaseActor _teleportBehindThis;

    public TeleportBehind(BaseActor teleportBehindThis)
    {
        _teleportBehindThis = teleportBehindThis;
    }

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new TeleportBehindConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Move, Actor, _teleportBehindThis)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        TeleportBehind actionClone = new(_teleportBehindThis)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}