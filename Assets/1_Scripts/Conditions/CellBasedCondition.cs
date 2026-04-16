using UnityEngine;

public static class CellBasedCondition
{
    public static bool ActorIsOnCellsAhead(DungeonMaster dungeonMaster, IActor actorToFind, IActor caster)
    {
        int cellIndex = caster.PositionCellIndex;
        bool facingRight = caster.IsFacingRight();

        if (facingRight)
        {
            for (int i = cellIndex; i < dungeonMaster.Cells.Count - 1; i++)
            {
                if (dungeonMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int i = cellIndex; i >= 0; i--)
            {
                if (dungeonMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool ActorInRangeOfXCells(DungeonMaster dungeonMaster, IActor actorToFind, IActor caster, int range)
    {
        int cellIndex = caster.PositionCellIndex;

        int rangeTemp = range;
        while (rangeTemp > 0)
        {
            if (rangeTemp + cellIndex < dungeonMaster.Cells.Count)
            {
                if (dungeonMaster.Cells[rangeTemp + cellIndex].EnityOccupyingThisCell == actorToFind)
                {
                    return true;
                }
            }
            if (cellIndex - rangeTemp >= 0)
            {
                if (dungeonMaster.Cells[cellIndex - rangeTemp].EnityOccupyingThisCell == actorToFind)
                {
                    return true;
                }
            }
            rangeTemp--;
        }
        return false;
    }

    public static bool CellAheadIsEmpty(DungeonMaster dungonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (CellAheadExists(dungonMaster, actor))
        {
            if (dungonMaster.CurrentActor.IsFacingRight())
            {
                return dungonMaster.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
            }
            else
            {
                return dungonMaster.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
            }
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }

    public static bool CellBehindIsEmpty(DungeonMaster dungonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (CellBehindExists(dungonMaster, actor))
        {
            if (dungonMaster.CurrentActor.IsFacingRight())
            {
                return dungonMaster.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
            }
            else
            {
                return dungonMaster.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
            }
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }

    public static bool CellAheadExists(DungeonMaster dungeonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (dungeonMaster.CurrentActor.IsFacingRight())
        {
            return currentCellIndex + 1 < dungeonMaster.Cells.Count;
        }
        else
        {
            return currentCellIndex > 0;
        }
    }

    public static bool CellBehindExists(DungeonMaster dungeonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (dungeonMaster.CurrentActor.IsFacingRight())
        {
            return currentCellIndex > 0;
        }
        else
        {
            return currentCellIndex + 1 < dungeonMaster.Cells.Count;
        }
    }

    public static bool AdjacentCellsExists(DungeonMaster dungeonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        return 0 < currentCellIndex && currentCellIndex + 1 < dungeonMaster.Cells.Count;
    }
}