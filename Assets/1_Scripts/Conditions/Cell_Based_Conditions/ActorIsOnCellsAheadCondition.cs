using UnityEngine;

public class ActorIsOnCellsAheadCondition : BaseConditionCommand
{
    private IActor _actorToFind;
    private IActor _caster;

    public ActorIsOnCellsAheadCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actorToFind, IActor caster)
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
            for (int i = cellIndex; i < LevelMaster.Cells.Count - 1; i++)
            {
                if (LevelMaster.Cells[i].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int i = cellIndex; i >= 0; i--)
            {
                if (LevelMaster.Cells[i].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
        }
        return false;
    }
}