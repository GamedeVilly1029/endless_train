using UnityEngine;

public class ShockWave : BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("AttackActionUI/ShockWaveActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("AttackActionUI/ShockWaveActionUI");
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
            new ShockWaveConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Attack, 5, Actor)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        ShockWave actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}