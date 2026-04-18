using UnityEngine;

public class StunPlayer : BaseAction 
{
    private IAction _beStunned = new BeStunned();
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

        _beStunned.InitializeAction(DungeonMasterInstance.Player, DungeonMasterInstance);

        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new();
        ActionAssignmentConstructElement stunPlayer = new(this, null, ActionAdditionConcrete.ChangeNextActionOfPlayer, ActionConcreteTag.Move, _beStunned);
        ActionConstruct.Add(stunPlayer);
    }

    public override IAction CreateClone(Transform transform)
    {
        MoveOneTileForward actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}