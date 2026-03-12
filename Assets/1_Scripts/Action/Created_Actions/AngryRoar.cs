using UnityEngine;

public class AngryRoar: BaseAction 
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("AngryRoarActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("AngryRoarActionUI");
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
            new ActionConstructElement(this, null, ActionConcrete.IncreaseDamageOfNextAttack, 5, ActionConcreteTag.Skill)
        };
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
    {
        AngryRoar actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}