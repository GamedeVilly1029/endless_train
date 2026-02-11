using UnityEngine;

public static class ActionConditions
{
    // CurrentCellIndex to the actor.
    public static bool CellAheadIsEmpty(DungeonMaster master)
    {
        int currentCellIndex = master.CurrentActor.PositionCellIndex;
        if (CellAheadExists(master))
        {
            if (master.CurrentActor.IsFacingRight)
            {
                return master.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
            }
            else
            {
                return master.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
            }
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }

    public static bool CellAheadExists(DungeonMaster master)
    {
        int currentCellIndex = master.CurrentActor.PositionCellIndex;
        if (master.CurrentActor.IsFacingRight)
        {
            return currentCellIndex + 1 < master.Cells.Count;
        }
        else
        {
            return currentCellIndex >= 0;
        }
    }
}