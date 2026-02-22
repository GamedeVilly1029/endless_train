using UnityEngine;

public static class ActionConditions
{
    public static bool CellAheadIsEmpty(DungeonMaster dungonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (CellAheadExists(dungonMaster, actor))
        {
            if (dungonMaster.CurrentActor.IsFacingRight)
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

    public static bool CellAheadExists(DungeonMaster dungeonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        if (dungeonMaster.CurrentActor.IsFacingRight)
        {
            return currentCellIndex + 1 < dungeonMaster.Cells.Count;
        }
        else
        {
            return currentCellIndex > 0;
        }
    }

    public static bool AdjacentCellsExists(DungeonMaster dungeonMaster, IActor actor)
    {
        int currentCellIndex = actor.PositionCellIndex;
        return 0 < currentCellIndex && currentCellIndex + 1 < dungeonMaster.Cells.Count;
    }

    public static bool PositionIndexChangedInPreviousAction(DungeonMaster master, IActor actor)
    {
        if (actor.PositionCellIndexHistory.Count >= 2)
        {
            int lastIndex = actor.PositionCellIndexHistory.Pop();
            int secondToLastIndex = actor.PositionCellIndexHistory.Peek();
            actor.PositionCellIndexHistory.Push(lastIndex);

            if (secondToLastIndex != lastIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static bool ConcreteHistoryIsEmpty(DungeonMaster dungeonMaster, IActor actor)
    {
        return dungeonMaster.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    }
}