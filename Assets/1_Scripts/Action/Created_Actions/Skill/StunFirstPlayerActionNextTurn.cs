using UnityEngine;

public class StunFirstPlayerActionNextTurn: BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StunFirstPlayerActionNextTurnConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        StunFirstPlayerActionNextTurn actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}