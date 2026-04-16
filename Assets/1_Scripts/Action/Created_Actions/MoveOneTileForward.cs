using UnityEngine;

public class MoveOneTileForward : BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("MovementActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("MovementActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentation Asset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new();
        ValueConstructElement moveForward = new(this, null, ActionConcrete.WalkXTilesForward, ActionConcreteTag.Move, 1);
        ActionConstruct.Add(moveForward);
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