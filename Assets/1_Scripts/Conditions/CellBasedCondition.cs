using UnityEngine;

public static class CellBasedCondition
{
    // private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    // private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static bool ActorIsOnCellsAhead(IActor actorToFind, IActor caster)
    // {
    //     int cellIndex = caster.PositionCellIndex;
    //     bool facingRight = caster.IsFacingRight();

    //     if (facingRight)
    //     {
    //         for (int i = cellIndex; i < _levelMaster.Cells.Count - 1; i++)
    //         {
    //             if (_levelMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         for (int i = cellIndex; i >= 0; i--)
    //         {
    //             if (_levelMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }

    // public static bool ActorInRangeOfXCellsFromOtherActor(IActor actorToFind, IActor caster, int range)
    // {
    //     int cellIndex = caster.PositionCellIndex;

    //     int rangeTemp = range;
    //     while (rangeTemp > 0)
    //     {
    //         if (rangeTemp + cellIndex < _levelMaster.Cells.Count)
    //         {
    //             if (_levelMaster.Cells[rangeTemp + cellIndex].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //         if (cellIndex - rangeTemp >= 0)
    //         {
    //             if (_levelMaster.Cells[cellIndex - rangeTemp].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //         rangeTemp--;
    //     }
    //     return false;
    // }

    // public static bool CellAheadIsEmpty(IActor actor)
    // {
    //     int currentCellIndex = actor.PositionCellIndex;
    //     if (CellAheadExists(actor))
    //     {
    //         if (_turnProcessor.CurrentActor.IsFacingRight())
    //         {
    //             return _levelMaster.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
    //         }
    //         else
    //         {
    //             return _levelMaster.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
    //         return false;
    //     }
    // }

    // public static bool CellBehindIsEmpty( IActor actor)
    // {
    //     int currentCellIndex = actor.PositionCellIndex;
    //     if (CellBehindExists(actor))
    //     {
    //         if (_turnProcessor.CurrentActor.IsFacingRight())
    //         {
    //             return _levelMaster.Cells[currentCellIndex - 1].EnityOccupyingThisCell == null;
    //         }
    //         else
    //         {
    //             return _levelMaster.Cells[currentCellIndex + 1].EnityOccupyingThisCell == null;
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Cell ahead doesn't exist, thus can't be checked for emptyness. Returning false");
    //         return false;
    //     }
    // }

    // public static bool CellAheadExists(IActor actor)
    // {
    //     int currentCellIndex = actor.PositionCellIndex;
    //     if (_turnProcessor.CurrentActor.IsFacingRight())
    //     {
    //         return currentCellIndex + 1 < _levelMaster.Cells.Count;
    //     }
    //     else
    //     {
    //         return currentCellIndex > 0;
    //     }
    // }

    // public static bool CellBehindExists( IActor actor)
    // {
    //     int currentCellIndex = actor.PositionCellIndex;
    //     if (_turnProcessor.CurrentActor.IsFacingRight())
    //     {
    //         return currentCellIndex > 0;
    //     }
    //     else
    //     {
    //         return currentCellIndex + 1 < _levelMaster.Cells.Count;
    //     }
    // }

    // public static bool AdjacentCellsExists(IActor actor)
    // {
    //     int currentCellIndex = actor.PositionCellIndex;
    //     return 0 < currentCellIndex && currentCellIndex + 1 < _levelMaster.Cells.Count;
    // }

    // public static bool ActorInRangeOfCells(IActor actor, int start, int end)
    // {
    //     for (int i = start; i <= end; i++)
    //     {
    //         if (actor.PositionCellIndex == i)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // public static bool ActorInRangeOfCellsAhead(IActor actorToFind, IActor caster)
    // {
    //     int cellIndex = caster.PositionCellIndex;
    //     bool facingRight = caster.IsFacingRight();

    //     if (facingRight)
    //     {
    //         for (int i = cellIndex; i < _levelMaster.Cells.Count - 1; i++)
    //         {
    //             if (_levelMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         for (int i = cellIndex; i >= 0; i--)
    //         {
    //             if (_levelMaster.Cells[i].EnityOccupyingThisCell == actorToFind)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }
}