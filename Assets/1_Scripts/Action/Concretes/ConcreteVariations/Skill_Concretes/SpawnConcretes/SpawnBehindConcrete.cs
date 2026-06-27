using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehindConcrete : BaseConcrete
{
    private BaseActor _caster;
    private EnemyInstantiationInfo _info;
    private int _idx;
    public SpawnBehindConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag,
        EnemyInstantiationInfo info,
        BaseActor caster
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
        _info = info;
        _caster = caster;
    }

    public override IEnumerator ChildExecute()
    {
        if (_caster.IsFacingRight())
        {
            _idx = _caster.PositionCellIndex - 1;
        }
        else
        {
            _idx = _caster.PositionCellIndex + 1;
        }

        yield return new SpawnConcrete(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, ActionConcreteTag.Skill, _idx, _info.RotationAngle, _info.ActorPrefab, _info.HP).Execute();
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SpawnBehindConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, _info, _caster);
    }
}