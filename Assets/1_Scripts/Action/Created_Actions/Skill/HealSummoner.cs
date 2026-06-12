using Unity.VisualScripting;
using UnityEngine;

public class HealSummoner : BaseAction 
{
    public Summoner Summoner;

    public HealSummoner(Summoner summoner)
    {
        Summoner = summoner;
    }

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new HealOtherConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, 5, Summoner)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        HealSummoner actionClone = new(Summoner)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
