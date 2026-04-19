using System.Collections;
using UnityEngine;

public static class GlobalLowLevelConcrete
{
    public static WaitForSeconds Pause = new WaitForSeconds(0.5f);

    public static IActor TryReturnActorAhead(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (CellBasedCondition.CellAheadExists(dungeonMaster, actor))
        {
            if (actor.IsFacingRight())
            {
                if (dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell != null)
                {
                    return dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell != null)
                {
                    return dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
        }
        else
        {
            // Debug.Log("Cell ahead isn't exists");
            return null;
        }
    }
  

}