using UnityEngine;

public class AllSummonDMGIncrease : BaseAction
{
    private Summoner _summoner;

    public AllSummonDMGIncrease(Summoner summoner)
    {
        _summoner = summoner;
    }

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new AllSummonDMGIncreaseConcrete
            (
                TurnProcessorInst, 
                LevelMasterInst, 
                this, 
                null, 
                ActionConcreteTag.Skill,
                _summoner
            )
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        AllSummonDMGIncrease actionClone = new(_summoner)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}