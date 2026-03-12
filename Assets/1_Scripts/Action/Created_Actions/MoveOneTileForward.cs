using UnityEngine;

public class MoveOneTileForward : BaseAction 
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
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
        ActionConstruct = new()
        {
            new ActionConstructElement(this, null, ActionConcrete.WalkXTiles, 1, ActionConcreteTag.Move)
        };
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
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