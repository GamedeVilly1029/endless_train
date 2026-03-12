using UnityEngine;
public class Push : BaseAction
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("PushActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("PushActionUI");
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
            new ActionConstructElement(this, null, ActionConcrete.Push, 0, ActionConcreteTag.Push)
        };
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
    {
        Push actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}