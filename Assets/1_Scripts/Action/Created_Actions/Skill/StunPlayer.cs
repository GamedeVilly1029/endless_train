using UnityEngine;

public class StunPlayer : BaseAction 
{
    private BaseAction _beStunned = new BeStunned();

    public override void InitializeChild()
    {
        _beStunned.Initialize(LevelMasterInst.Player, TurnProcessorInst, LevelMasterInst, 0, "SkillActionUI/Stun");
    }

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new ChangeNextActionOfPlayerConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, _beStunned)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        StunPlayer actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}