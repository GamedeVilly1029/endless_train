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
        ActionConstruct = new()
        {
            new StrikeConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Attack, 5),
            new PushConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Push),
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