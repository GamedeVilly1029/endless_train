using UnityEngine;
public class Push : BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 1;
        Cooldown = 0;
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
            new BaseConstructElement(this, null, PushConcrete.Push, ActionConcreteTag.Push)
        };
    }

    public override IAction CreateClone(Transform transform)
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