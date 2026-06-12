using UnityEngine;

public class Heal : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new HealPercentConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, 15, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Heal actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}