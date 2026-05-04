using UnityEngine;

public class CellAheadIsEmptyCondition : BaseConditionCommand
{
    private IActor _actor;

    public CellAheadIsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        if (new CellAheadExistsCondition(TurnProcessor, LevelMaster, _actor).Execute())
        {
            if (TurnProcessor.CurrentActor.IsFacingRight())
            {
                return LevelMaster.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
            }
            else
            {
                return LevelMaster.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
            }
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }
}