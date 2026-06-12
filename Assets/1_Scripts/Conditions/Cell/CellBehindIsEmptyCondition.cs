using UnityEngine;

public class CellBehindIsEmptyCondition : BaseConditionCommand
{
    private BaseActor _actor;

    public CellBehindIsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        if (new CellBehindExistsCondition(TurnProcessorInst, LevelMasterInst, _actor).Execute())
        {
            if (TurnProcessorInst.CurrentActor.IsFacingRight())
            {
                return LevelMasterInst.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
            }
            else
            {
                return LevelMasterInst.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
            }
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }
}