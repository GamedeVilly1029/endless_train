using UnityEngine;

public class AngryRoar: BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new IncreaseDamageOfNextAttackConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        AngryRoar actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}