using UnityEngine;

public class Strike : BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
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
            new ValueConstructElement(this, null, AttackConcrete.StrikeConcrete, ActionConcreteTag.Attack, 5)
        };
    }

    public override IAction CreateClone(Transform transform)
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