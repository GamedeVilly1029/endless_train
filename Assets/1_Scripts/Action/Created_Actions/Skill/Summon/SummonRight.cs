using UnityEngine;

public class SummonRight : BaseAction 
{
    private Summoner _summoner;
    private EnemyInstantiationInfo _enemyInstantiationInfo;

    public SummonRight(Summoner summoner, EnemyInstantiationInfo enemyInstantiationInfo)
    {
        _summoner = summoner;
        _enemyInstantiationInfo = enemyInstantiationInfo;
    }

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new SummonConcrete
            (
                TurnProcessorInst, 
                LevelMasterInst, 
                this, 
                null, 
                ActionConcreteTag.Skill, 
                _enemyInstantiationInfo, 
                _summoner, 
                _summoner.PositionCellIndex + 1
            )
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        SummonRight actionClone = new(_summoner, _enemyInstantiationInfo)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
