using UnityEngine;

public static class ActorPosManipulation
{
    public static void ChangeIndexAndPosition(DungeonMaster dungeonMaster, IActor actor)
    {
        if (actor.IsFacingRight)
        {
            dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
            dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.Transform.position = dungeonMaster.Cells[actor.PositionCellIndex + 1].CellPosition;
            actor.PositionCellIndex += 1;
        }
        else
        {
            dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
            dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.Transform.position = dungeonMaster.Cells[actor.PositionCellIndex - 1].CellPosition;
            actor.PositionCellIndex -= 1;
        }
    }

    public static void PushActor(DungeonMaster dungeonMaster, IActor actor, bool pushRight)
    {
        if (pushRight)
        {
            dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
            dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.Transform.position = dungeonMaster.Cells[actor.PositionCellIndex + 1].CellPosition;
            actor.PositionCellIndex += 1;
        }
        else
        {
            dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
            dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.Transform.position = dungeonMaster.Cells[actor.PositionCellIndex - 1].CellPosition;
            actor.PositionCellIndex -= 1;
        }
    }
}