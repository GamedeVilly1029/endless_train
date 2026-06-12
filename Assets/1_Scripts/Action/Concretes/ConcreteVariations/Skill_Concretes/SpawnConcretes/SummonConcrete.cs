using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonConcrete : BaseConcrete
{
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
        SpawnConcrete concrete = new(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, Tag, _idx, _info.RotationAngle, _info.ActorPrefab, _info.HP);
        yield return concrete.Execute();

        Summon summon = (Summon)concrete.Spawned;
        summon.SummonerInst = _summoner;
        _summoner.Summons.Add(summon);
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SummonConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _info, _summoner, _idx);
    }
}