using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConcrete : BaseConcrete
{
    private int _idx;
    private BaseActor _prefab;
    private float _rotation;
    private int _hp;

    public BaseActor Spawned;
    public SpawnConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag,
        int idx,
        float rotation,
        BaseActor prefab,
        int hp
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _prefab = prefab;
        _idx = idx;
        _hp = hp;
        _rotation = rotation;
    }

    public override List<IConditionCommand> CreateBaseConditionList()
    {
        List<IConditionCommand> conds = new()
        {
            new CellAtIdxExists(TurnProcessorInst, LevelMasterInst, _idx),
            new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _idx)
        };

        return conds;
    }

    public override IEnumerator ChildExecute()
    {
        Spawned = Object.Instantiate(_prefab, LevelMasterInst.InstantiationPlaceForEnemies);
        Spawned.Initialize(_idx, _rotation, _hp, TurnProcessorInst, LevelMasterInst);
        LevelMasterInst.AllActors.Add(Spawned);
        yield return new SpawnGrowEffect(Spawned, 0.25f).Execute();
    }

    public override IEnumerator DeclinedConcrete()
    {
        Debug.Log("Cell for this idx is either non-empty or non-existing");
        yield return null;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SpawnConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, _idx, _rotation, _prefab, _hp);
    }
}