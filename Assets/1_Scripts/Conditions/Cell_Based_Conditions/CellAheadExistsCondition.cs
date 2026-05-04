using UnityEngine;

public class CellAheadExistsCondition : BaseConditionCommand
{
    private IActor _actorToCheckFrom;
    public CellAheadExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actorToCheckFrom):base(turnProcessor, levelMaster)
    {
        _actorToCheckFrom = actorToCheckFrom;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actorToCheckFrom.PositionCellIndex;
        if (TurnProcessor.CurrentActor.IsFacingRight())
        {
            return currentCellIndex + 1 < LevelMaster.Cells.Count;
        }
        else
        {
            return currentCellIndex > 0;
        }
    }
}