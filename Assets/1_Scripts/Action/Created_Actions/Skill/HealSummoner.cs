using UnityEngine;

public class HealSummoner : BaseAction 
{
    public BaseActor Summoner;
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        // if (Resources.Load<GameObject>("SkillActionUI/Heal") != null)
        // {
        //     UIRepresentation = Resources.Load<GameObject>("SkillActionUI/Heal");
        // }
        // else
        // {
        //     Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        // }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new HealOtherConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Skill, 5, Summoner)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        HealSummoner actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
