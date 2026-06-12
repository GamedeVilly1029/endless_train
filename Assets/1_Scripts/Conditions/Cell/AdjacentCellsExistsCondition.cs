using UnityEngine;

public class AdjacentCellsExistsCondition : BaseConditionCommand
{
    private BaseActor _actor;

    public AdjacentCellsExistsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        int currentCellIndex = _actor.PositionCellIndex;
        return 0 < currentCellIndex && currentCellIndex + 1 < LevelMasterInst.Cells.Count;
    }
}