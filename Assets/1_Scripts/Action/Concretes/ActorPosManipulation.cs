using System.Collections;
using UnityEngine;

public static class ActorPosManipulation
{
    public static IEnumerator ChangeIndexAndPosition(DungeonMaster dungeonMaster, IActor actor, MoveData moveData)
    {
        dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        Vector2 end = DetermineEnd_AdjustInfo(dungeonMaster, actor);

        yield return StepArc(actor, moveData, start, end);
        actor.Transform.position = end;
    }

    private static Vector2 DetermineEnd_AdjustInfo(DungeonMaster dungeonMaster, IActor actor)
    {
        if (actor.IsFacingRight)
        {
            dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }
    private static IEnumerator StepArc(IActor actor, MoveData moveData, Vector2 start, Vector2 end)
    {
        float timePast = 0;
        while (timePast <= moveData.Duration)
        {
            float normalizedTime = timePast / moveData.Duration;
            Vector2 newPos = Vector2.Lerp(start, end, normalizedTime);
            newPos.y = start.y + moveData.Curve.Evaluate(normalizedTime);

            actor.Transform.position = newPos;
            timePast += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator PushActor(DungeonMaster dungeonMaster, IActor actorToPush, bool pushRight)
    {
        dungeonMaster.Cells[actorToPush.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = dungeonMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        Vector2 end = DeterminePushEnd_AdjustInfo(dungeonMaster, actorToPush, pushRight);

        ParticlePlayer.StartBePushed(actorToPush);

        yield return StepFlat(actorToPush, start, end);
        actorToPush.Transform.position = end;

        // ParticlePlayer.StopBePushed(actorToPush);
    }

    private static Vector2 DeterminePushEnd_AdjustInfo(DungeonMaster dungeonMaster, IActor actorToPush, bool pushRight)
    {
        if (pushRight)
        {
            dungeonMaster.Cells[actorToPush.PositionCellIndex + 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex += 1;
            return dungeonMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
        else
        {
            dungeonMaster.Cells[actorToPush.PositionCellIndex - 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex -= 1;
            return dungeonMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
    }

    private static IEnumerator StepFlat(IActor actor, Vector2 start, Vector2 end)
    {
        float timePast = 0;
        while (timePast <= 0.2f)
        {
            float normalizedTime = timePast / 0.2f;
            Vector2 newPos = Vector2.Lerp(start, end, normalizedTime);

            actor.Transform.position = newPos;
            timePast += Time.deltaTime;
            yield return null;
        }
    }
}