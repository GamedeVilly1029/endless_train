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
            new ValueConstructElement(this, null, SkillConcrete.IncreaseDamageOfNextAttack, ActionConcreteTag.Skill, 5)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        AngryRoar actionClone = new()
        {
            DungeonMasterInstance = DungeonMasterInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}