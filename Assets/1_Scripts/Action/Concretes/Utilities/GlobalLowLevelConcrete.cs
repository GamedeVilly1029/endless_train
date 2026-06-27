using System.Collections;
using UnityEngine;

public static class GlobalLowLevelConcrete
{
    public static WaitForSeconds Pause = new WaitForSeconds(0.5f);

    public static BaseActor TryReturnActorAhead(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor)
    {
        IConditionCommand cellAheadExists = new CellAheadExistsCondition(turnProcessor , levelMaster, actor);
        if (cellAheadExists.Execute())
        {
            if (actor.IsFacingRight())
            {
                if (levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell != null)
                {
                    return levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell != null)
                {
                    return levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
        }
        else
        {
            return null;
        }
    }

    public static BaseActor TryReturnFirstActorOnCellsAhead(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor)
    {
        if (actor.IsFacingRight())
        {
            for (int i = actor.PositionCellIndex + 1; i < levelMaster.Cells.Count; i++)
            {
                if (!new CellAtIdxIsEmpty(turnProcessor, levelMaster, i).Execute())
                {
                    return levelMaster.Cells[i].EnityOccupyingThisCell;
                }
            }
            return null;
        }
        else
        {
            for (int i = actor.PositionCellIndex - 1; i >= 0; i--)
            {
                if (!new CellAtIdxIsEmpty(turnProcessor, levelMaster, i).Execute())
                {
                    return levelMaster.Cells[i].EnityOccupyingThisCell;
                }
            }
            return null;
        }
    }
}