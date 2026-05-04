using UnityEngine;

public class AdjacentCellsExistsCondition : BaseConditionCommand
{
    private IActor _actor;

    public AdjacentCellsExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        return 0 < currentCellIndex && currentCellIndex + 1 < LevelMaster.Cells.Count;
    }
}