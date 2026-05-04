using System.Collections;
using UnityEngine;

public static class GlobalLowLevelConcrete
{
    public static WaitForSeconds Pause = new WaitForSeconds(0.5f);

    public static IActor TryReturnActorAhead(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
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
            // Debug.Log("Cell ahead isn't exists");
            return null;
        }
    }
  

}