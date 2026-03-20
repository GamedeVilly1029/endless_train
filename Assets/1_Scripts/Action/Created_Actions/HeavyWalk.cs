using UnityEngine;

public class HeavyWalk : BaseAction 
{

    public override void InitializeAction(IActor actor, DungeonMaster dungeonMaster)
    {
        DungeonMasterInstance = dungeonMaster;
        Actor = actor;
        if (Resources.Load<GameObject>("HeavyWalkActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("HeavyWalkActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        // Here, what to do.
        ActionConstruct = new();
        ActionConstructElement elem1 = new(this, null, ActionConcrete.StrikeConcrete, 4, ActionConcreteTag.Attack);
        ActionConstruct.Add(elem1);

        ActionConstructElement elem2 = new(this, null, ActionConcrete.Push, 0, ActionConcreteTag.Push);
        ActionConstruct.Add(elem2);
        
        ActionConstructElement elem3 = new(this, null, ActionConcrete.WalkXTiles, 1, ActionConcreteTag.Move);
        ActionConstruct.Add(elem3);
    }

    public override IAction CloneAndInstantiateUI(Transform transform)
    {
        HeavyWalk actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}