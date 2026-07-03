using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehindConcrete : BaseConcrete
{
    private BaseActor _caster;
    private BaseActor _target;
    public TeleportBehindConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag,
        BaseActor caster,
        BaseActor target 
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
        _caster = caster;
        _target = target;
    }

    public override List<IConditionCommand> CreateBaseConditionList()
    {
        IConditionCommand targetRight = new TargetIsRightFromCaster(TurnProcessorInst, LevelMasterInst, _caster, _target);
        return new List<IConditionCommand>()
        {
            new ActorInRangeOfCellsAheadCondition(TurnProcessorInst, LevelMasterInst, _target, _caster),
            new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, targetRight.Execute() ? _target.PositionCellIndex + 1 : _target.PositionCellIndex - 1)
        };  
    }
    

    public override IEnumerator DeclinedConcrete()
    {
        Debug.Log("Actor isn't in range of cells ahead");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IEnumerator ChildExecute()
    {
        Vector3 startScale = _caster.GraphicTransform.localScale;
        IConditionCommand targetRight = new TargetIsRightFromCaster(TurnProcessorInst, LevelMasterInst, _caster, _target);

        float rotation = _caster.IsFacingRight() ? 180 : 0;
        int idx = targetRight.Execute() ? _target.PositionCellIndex + 1 : _target.PositionCellIndex - 1;

        yield return new ScaleLerpUtility(0.25f, _caster.GraphicTransform, startScale, Vector3.zero).Execute();

        LevelMasterInst.Cells[_caster.PositionCellIndex].EnityOccupyingThisCell = null;
        _caster.TransformReference.position = LevelMasterInst.Cells[idx].CellPosition;
        _caster.GraphicTransform.rotation = Quaternion.Euler(0f, rotation, 0f);

        _caster.PositionCellIndex = idx;
        LevelMasterInst.Cells[_caster.PositionCellIndex].EnityOccupyingThisCell = _caster;

        yield return new ScaleLerpUtility(0.25f, _caster.GraphicTransform, Vector3.zero, startScale).Execute();
    }
}