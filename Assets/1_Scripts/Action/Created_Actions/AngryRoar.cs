using UnityEngine;

public class AngryRoar: BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 1;
        Cooldown = 0;
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
            new IncreaseDamageOfNextAttackConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Skill)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        AngryRoar actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}