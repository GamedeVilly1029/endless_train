using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieConcrete : BaseConcrete
{
    BaseActor _suicider;
    public DieConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag,
        BaseActor suicider
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _suicider = suicider;
    }

    public override IEnumerator ChildExecute()
    {
        _suicider.IsDead = true;
        _suicider.ActionRowInst.Actions.Clear();
        _suicider.LevelMasterInst.Cells[_suicider.PositionCellIndex].EnityOccupyingThisCell = null;
        _suicider.gameObject.SetActive(false); 
        yield break;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new DieConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _suicider);
    }
}
