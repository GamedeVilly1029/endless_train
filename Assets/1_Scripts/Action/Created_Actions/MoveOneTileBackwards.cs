using JetBrains.Annotations;
using UnityEngine;

public class MoveOneTileBackwards : BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;

        if (Resources.Load<GameObject>("MovementBackwardsActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("MovementBackwardsActionUI");
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
            new ValueConstructElement(this, null, MovementConcrete.WalkXTilesBackwards, ActionConcreteTag.Move, 1)
        };
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
