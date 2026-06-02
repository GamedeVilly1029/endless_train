using UnityEngine;

public class ActorInRangeOfCellsAheadCondition : BaseConditionCommand
{
    private BaseActor _actorToFind;
    private BaseActor _caster;

    public ActorInRangeOfCellsAheadCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actorToFind, BaseActor caster)
        : base(turnProcessor, levelMaster)
    {
        _actorToFind = actorToFind;
        _caster = caster;
    }

    public override bool Execute()
    {
        int cellIndex = _caster.PositionCellIndex;
        bool facingRight = _caster.IsFacingRight();

        if (facingRight)
        {
            for (int i = cellIndex; i < LevelMasterInst.Cells.Count - 1; i++)
            {
                if (LevelMasterInst.Cells[i].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int i = cellIndex; i >= 0; i--)
            {
                if (LevelMasterInst.Cells[i].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
        }
        return false;
    }
}