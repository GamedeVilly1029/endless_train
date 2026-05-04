using UnityEngine;

public class CellBehindExistsCondition : BaseConditionCommand
{
    private IActor _actor;

    public CellBehindExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        if (TurnProcessor.CurrentActor.IsFacingRight())
        {
            return currentCellIndex > 0;
        }
        else
        {
            return currentCellIndex + 1 < LevelMaster.Cells.Count;
        }
    }
}