using UnityEngine;

public class CellBehindExistsCondition : BaseConditionCommand
{
    private BaseActor _actor;

    public CellBehindExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        if (TurnProcessorInst.CurrentActor.IsFacingRight())
        {
            return currentCellIndex > 0;
        }
        else
        {
            return currentCellIndex + 1 < LevelMasterInst.Cells.Count;
        }
    }
}