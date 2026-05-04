using UnityEngine;

public class StunFirstPlayerActionNextTurn: BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("Stun") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("Stun");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentation Asset");
        }

        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new StunFirstPlayerActionNextTurnConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Skill)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        MoveOneTileForward actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
