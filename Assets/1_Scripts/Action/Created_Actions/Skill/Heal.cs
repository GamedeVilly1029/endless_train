using UnityEngine;

public class Heal : BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("SkillActionUI/Heal") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("SkillActionUI/Heal");
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
            new HealPercentConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Skill, 15, Actor)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        Heal actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}