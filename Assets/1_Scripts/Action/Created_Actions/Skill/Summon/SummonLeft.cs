using System.Collections.Generic;
using UnityEngine;

public class SummonLeft : BaseAction 
{
    private Summoner _summoner;
    private EnemyInstantiationInfo _enemyInstantiationInfo;

    public SummonLeft(Summoner summoner, EnemyInstantiationInfo enemyInstantiationInfo)
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
                _summoner.PositionCellIndex - 1
            )
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        SummonLeft actionClone = new(_summoner, _enemyInstantiationInfo)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}