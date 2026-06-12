using UnityEngine;

public class BeStunned : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new BeStunnedConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        BeStunned actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}