using UnityEngine;

public class Strike : BaseAction 
{
    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("AttackActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("AttackActionUI");
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
            new ActionConstructElement(this, null, ActionConcrete.HitActorAhead, 5, ActionConcreteTag.Attack)
        };
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
    {
        Strike actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}