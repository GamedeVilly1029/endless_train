using UnityEngine;

public class Rotate : BaseAction 
{
    
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("RotateActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("RotateActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ActionConstructElement(this, null, ActionConcrete.RotateActor, 0, ActionConcreteTag.Move)
        };
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
    {
        Rotate actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
