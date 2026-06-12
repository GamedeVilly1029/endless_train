using UnityEngine;

public class Die : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new DieConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Die actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
