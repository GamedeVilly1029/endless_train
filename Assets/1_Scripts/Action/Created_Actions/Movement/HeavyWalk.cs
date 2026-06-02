using UnityEngine;

public class HeavyWalk : BaseAction 
{

    public override void InitializeChildAction()
    {
        CooldownMax = 1;
        Cooldown = 0;
        if (Resources.Load<GameObject>("MovementActionUI/HeavyWalkActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("MovementActionUI/HeavyWalkActionUI");
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
            new StrikeConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Attack, 5, Actor),
            new PushConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Push, Actor),
            new StepXTilesForwardConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Move, 1, Actor)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        HeavyWalk actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}