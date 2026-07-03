using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonConcrete : BaseConcrete
{
    public Summon Summon;
    private EnemyInstantiationInfo _info;
    private Summoner _summoner;
    private int _idx;
    public SummonConcrete
    (
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        BaseAction actionOfThisConcrete,
        List<IConditionCommand> extraConditions,
        ActionConcreteTag tag,
        EnemyInstantiationInfo info,
        Summoner summoner,
        int idx
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _info = info;
        _summoner = summoner;
        _idx = idx;
    }

    public override IEnumerator ChildExecute()
    {
        Summon = Object.Instantiate(_info.ActorPrefab as Summon, LevelMasterInst.InstantiationPlaceForEnemies);
        Summon.InitializeSummon(_summoner);
        Summon.Initialize(_idx, _info.RotationAngle, _info.HP, TurnProcessorInst, LevelMasterInst);

        _summoner.Summons.Add(Summon);
        LevelMasterInst.AllActors.Add(Summon);

        yield return new SpawnGrowEffect(Summon, 0.25f).Execute();
    }

    public override List<IConditionCommand> CreateBaseConditionList()
    {
        List<IConditionCommand> baseConditions = new()
        {
            new CellAtIdxExists(TurnProcessorInst, LevelMasterInst, _idx),
            new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _idx),
            new IsSummonCondition(TurnProcessorInst, LevelMasterInst, _info.ActorPrefab)
        };

        return baseConditions;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SummonConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _info, _summoner, _idx);
    }
}