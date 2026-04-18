using UnityEngine;

public class Rotate : BaseAction 
{
    
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
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
            new BaseConstructElement(this, null, MovementConcrete.RotateConcrete, ActionConcreteTag.Move)
        };
    }

    public override IAction CreateClone(Transform transform)
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
