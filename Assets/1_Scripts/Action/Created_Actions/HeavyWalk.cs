using UnityEngine;

public class HeavyWalk : BaseAction 
{

    public override void InitializeChildAction()
    {
        CooldownMax = 1;
        Cooldown = 0;
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
        ValueConstructElement elem1 = new(this, null, AttackConcrete.StrikeConcrete, ActionConcreteTag.Attack, 4);
        ActionConstruct.Add(elem1);

        BaseConstructElement elem2 = new(this, null, PushConcrete.Push, ActionConcreteTag.Push);
        ActionConstruct.Add(elem2);
        
        ValueConstructElement elem3 = new(this, null, MovementConcrete.WalkXTilesForward, ActionConcreteTag.Move, 1);
        ActionConstruct.Add(elem3);
    }

    public override IAction CreateClone(Transform transform)
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