using UnityEngine;

public static class CellBasedCondition
{
    public static bool ActorIsOnCellsAhead(DungeonMaster dungeonMaster, IActor actorToFind, IActor caster)
    {
        int cellIndex = caster.PositionCellIndex;
        bool facingRight = caster.IsFacingRight;

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
}