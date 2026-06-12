using UnityEngine;

public class CellAheadExistsCondition : BaseConditionCommand
{
    private BaseActor _actorToCheckFrom;
    public CellAheadExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actorToCheckFrom):base(turnProcessor, levelMaster)
    {
        _actorToCheckFrom = actorToCheckFrom;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actorToCheckFrom.PositionCellIndex;
        if (TurnProcessorInst.CurrentActor.IsFacingRight())
        {
            return currentCellIndex + 1 < LevelMasterInst.Cells.Count;
        }
        else
        {
            return currentCellIndex > 0;
        }
    }
}