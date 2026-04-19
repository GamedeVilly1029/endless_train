using System.Collections;
using UnityEngine;

public static class MovementLowLevelConcrete
{
    private static IEnumerator StepArc(IActor actor, MoveData moveData, Vector2 start, Vector2 end)
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

    private static IEnumerator StepFlat(IActor actor, Vector2 start, Vector2 end, float stepDuration)
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
    }

    public static IEnumerator StepForwardOrBackwards(DungeonMaster dungeonMaster, IActor actor, MoveData moveData, bool forward)
    {
        dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        Vector2 end;
        if (forward)
        {
            end = StepForwardCalculator(dungeonMaster, actor);
        }
        else
        {
            end = StepBackwardsCalculator(dungeonMaster, actor);
        }

        yield return StepArc(actor, moveData, start, end);
        actor.TransformReference.position = end;
    }

    private static Vector2 StepForwardCalculator(DungeonMaster dungeonMaster, IActor actor)
    {
        if (actor.IsFacingRight())
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

    private static Vector2 StepBackwardsCalculator(DungeonMaster dungeonMaster, IActor actor)
    {
        if (actor.IsFacingRight())
        {
            dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }

    public static IEnumerator Slide(DungeonMaster dungeonMaster, IActor actor, int destinationCellIDX)
    {
        Vector2 start = dungeonMaster.Cells[actor.PositionCellIndex].CellPosition;
        Vector2 end = dungeonMaster.Cells[destinationCellIDX].CellPosition;

        dungeonMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
        dungeonMaster.Cells[destinationCellIDX].EnityOccupyingThisCell = actor;
        actor.PositionCellIndex = destinationCellIDX;

        ParticlePlayer.StartSlide(actor);
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("slide");
        yield return StepFlat(actor, start, end, 0.2f);
        actor.TransformReference.position = end;
        ParticlePlayer.StopSlide(actor);
    }

    public static IEnumerator BePushed(DungeonMaster dungeonMaster, IActor actorToPush, bool pushRight)
    {
        dungeonMaster.Cells[actorToPush.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = dungeonMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        Vector2 end = DeterminePushEnd_AdjustInfo(dungeonMaster, actorToPush, pushRight);

        ParticlePlayer.StartBePushed(actorToPush);

        yield return StepFlat(actorToPush, start, end, 0.2f);
        actorToPush.TransformReference.position = end;

        ParticlePlayer.StopBePushed(actorToPush);
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

    public static IEnumerator RotateActor(IActor actor)
    {
        Quaternion start = actor.TransformReference.rotation;
        Quaternion target = actor.IsFacingRight() ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        float timePast = 0;
        while (timePast < 0.25)
        {
            float normalizedTime = timePast / 0.25f;
            actor.TransformReference.rotation = Quaternion.Slerp(start, target, normalizedTime);
            timePast += Time.deltaTime;
            yield return null;
        }
        actor.TransformReference.rotation = target;
    }
    
    public static IEnumerator MoveOneCellForward(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!CellBasedCondition.CellAheadExists(dungeonMaster, actor))
        {
            yield break;
        }
        else if (!CellBasedCondition.CellAheadIsEmpty(dungeonMaster, actor))
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return MovementLowLevelConcrete.StepForwardOrBackwards(dungeonMaster, actor, Resources.Load<MoveData>("StepData"), true);
        }
        yield return GlobalLowLevelConcrete.Pause;
    }

    public static IEnumerator MoveOneCellBackwards(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!CellBasedCondition.CellBehindExists(dungeonMaster, actor))
        {
            yield break;
        }
        else if (!CellBasedCondition.CellBehindIsEmpty(dungeonMaster, actor))
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return MovementLowLevelConcrete.StepForwardOrBackwards(dungeonMaster, actor, Resources.Load<MoveData>("StepData"), false);
        }
        yield return GlobalLowLevelConcrete.Pause;
    }
}