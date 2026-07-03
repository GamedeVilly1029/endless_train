using System.Collections;
using UnityEngine;

public static class MovementLowLevelConcrete
{
    public static IEnumerator StepArc(BaseActor actor, MoveData moveData, Vector2 start, Vector2 end)
    {
        float timePast = 0;
        while (timePast <= moveData.Duration)
        {
            float normalizedTime = timePast / moveData.Duration;
            Vector2 newPos = Vector2.Lerp(start, end, normalizedTime);
            newPos.y = start.y + moveData.Curve.Evaluate(normalizedTime);

            actor.TransformReference.position = newPos;
            timePast += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator StepFlat(BaseActor actor, Vector2 start, Vector2 end, float stepDuration)
    {
        float timePast = 0;
        while (timePast <= stepDuration)
        {
            float normalizedTime = timePast / stepDuration;
            Vector2 newPos = Vector2.Lerp(start, end, normalizedTime);

            actor.TransformReference.position = newPos;
            timePast += Time.deltaTime;
            yield return null;
        }
        actor.TransformReference.position = end;
    }


    public static Vector2 StepForwardCalculator(LevelMaster levelMaster, BaseActor actor)
    {
        if (actor.IsFacingRight())
        {
            levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }

    public static Vector2 StepBackwardsCalculator(LevelMaster levelMaster, BaseActor actor)
    {
        if (actor.IsFacingRight())
        {
            levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }



    public static Vector2 BePushedCalculator(LevelMaster levelMaster, BaseActor actorToPush, bool pushRight)
    {
        if (pushRight)
        {
            levelMaster.Cells[actorToPush.PositionCellIndex + 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex += 1;
            return levelMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
        else
        {
            levelMaster.Cells[actorToPush.PositionCellIndex - 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex -= 1;
            return levelMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
    }

    public static IEnumerator StepForwardOrBackwards(LevelMaster levelMaster, BaseActor actor, MoveData moveData, bool forward)
    {
        levelMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        Vector2 end;
        if (forward)
        {
            end = StepForwardCalculator(levelMaster, actor);
        }
        else
        {
            end = StepBackwardsCalculator(levelMaster, actor);
        }

        yield return StepArc(actor, moveData, start, end);
        actor.TransformReference.position = end;
    }
}