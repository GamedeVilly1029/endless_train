using UnityEngine;

public static class ActionConditions
{
    public static bool CellAheadIsEmpty(DungeonMaster master, int currentCellIndex)
    {
        if (CellAheadExists(master, currentCellIndex))
        {
            return !master.Cells[currentCellIndex + 1].IsOcupiedByEntity;
        }
        else
        {
            Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
            return false;
        }
    }

    public static bool CellAheadExists(DungeonMaster master, int currentCellIndex)
    {
        return currentCellIndex + 1 < master.Cells.Count;
    }
}