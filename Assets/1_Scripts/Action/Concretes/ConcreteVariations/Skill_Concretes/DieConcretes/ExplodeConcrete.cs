using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeConcrete : ValueConcrete
{
    private BaseActor _exploder;
    public ExplodeConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag, 
        int value,
        BaseActor exploder
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _exploder = exploder;
    }

    public override IEnumerator ChildExecute()
    {
        yield return DamageOneSide(1);
        yield return DamageOneSide(-1);

        _exploder.DeathPlayer = new ParticleActorExplodePlayer(_exploder);

        yield return new DieInvincibleConcrete(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, ActionConcreteTag.Skill, _exploder).Execute();
        yield return GlobalLowLevelConcrete.Pause;
    }

    private IEnumerator DamageOneSide(int idx)
    {
        if (!new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _exploder.PositionCellIndex + idx).Execute())
        {
            BaseActor damageTaker = LevelMasterInst.Cells[_exploder.PositionCellIndex + idx].EnityOccupyingThisCell;
            yield return damageTaker.TakeBluntDamage(Value);
        }
        else
        {
            Debug.Log($"Cell: {_exploder.PositionCellIndex + idx} is either empty or non existing");
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new ExplodeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _exploder);
    }
}