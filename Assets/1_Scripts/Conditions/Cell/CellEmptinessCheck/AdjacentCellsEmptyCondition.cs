using UnityEngine;

public class AdjacentCellsEmptyCondition : BaseConditionCommand
{
    private BaseActor _caster;
    public AdjacentCellsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor caster) : base(turnProcessor, levelMaster)
    {
        _caster = caster;
    }

    public override bool Execute()
    {
        if
        (
            new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex - 1).Execute() ||
            new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex + 1).Execute()
        )
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}