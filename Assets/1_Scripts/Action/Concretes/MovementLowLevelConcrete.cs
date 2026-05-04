using System.Collections;
using UnityEngine;

public static class MovementLowLevelConcrete
{
    private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    public static IEnumerator StepArc(IActor actor, MoveData moveData, Vector2 start, Vector2 end)
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

    public static IEnumerator StepFlat(IActor actor, Vector2 start, Vector2 end, float stepDuration)
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


    public static Vector2 StepForwardCalculator(IActor actor)
    {
        if (actor.IsFacingRight())
        {
            _levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            _levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }

    public static Vector2 StepBackwardsCalculator(IActor actor)
    {
        if (actor.IsFacingRight())
        {
            _levelMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex -= 1;
            return _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
        else
        {
            _levelMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell = actor;
            actor.PositionCellIndex += 1;
            return _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        }
    }



    public static Vector2 BePushedCalculator(IActor actorToPush, bool pushRight)
    {
        if (pushRight)
        {
            _levelMaster.Cells[actorToPush.PositionCellIndex + 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex += 1;
            return _levelMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
        else
        {
            _levelMaster.Cells[actorToPush.PositionCellIndex - 1].EnityOccupyingThisCell = actorToPush;
            actorToPush.PositionCellIndex -= 1;
            return _levelMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
        }
    }

    public static IEnumerator StepForwardOrBackwards(IActor actor, MoveData moveData, bool forward)
    {
        _levelMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
        Vector2 end;
        if (forward)
        {
            end = StepForwardCalculator(actor);
        }
        else
        {
            end = StepBackwardsCalculator(actor);
        }

        yield return StepArc(actor, moveData, start, end);
        actor.TransformReference.position = end;
    }   

//========================================================================================================================================================================
    

    // public static IEnumerator Slide(IActor actor, int destinationCellIDX)
    // {
    //     Vector2 start = _levelMaster.Cells[actor.PositionCellIndex].CellPosition;
    //     Vector2 end = _levelMaster.Cells[destinationCellIDX].CellPosition;

    //     _levelMaster.Cells[actor.PositionCellIndex].EnityOccupyingThisCell = null;
    //     _levelMaster.Cells[destinationCellIDX].EnityOccupyingThisCell = actor;
    //     actor.PositionCellIndex = destinationCellIDX;

    //     ParticlePlayer.StartSlide(actor);
    //     Object.FindFirstObjectByType<AudioMaster>().PlaySound("slide");
    //     yield return StepFlat(actor, start, end, 0.2f);
    //     actor.TransformReference.position = end;
    //     ParticlePlayer.StopSlide(actor);
    // }

    // public static IEnumerator BePushed(IActor actorToPush, bool pushRight)
    // {
    //     _levelMaster.Cells[actorToPush.PositionCellIndex].EnityOccupyingThisCell = null;
    //     Vector2 start = _levelMaster.Cells[actorToPush.PositionCellIndex].CellPosition;
    //     Vector2 end = BePushedCalculator(actorToPush, pushRight);

    //     ParticlePlayer.StartBePushed(actorToPush);

    //     yield return StepFlat(actorToPush, start, end, 0.2f);
    //     actorToPush.TransformReference.position = end;

    //     ParticlePlayer.StopBePushed(actorToPush);
    // }
    
    // public static IEnumerator MoveOneCellForward(IConstructElement element)
    // {
    //     IActor actor = _turnProcessor.CurrentActor;
    //     if (!CellBasedCondition.CellAheadExists(actor))
    //     {
    //         yield break;
    //     }
    //     else if (!CellBasedCondition.CellAheadIsEmpty(actor))
    //     {
    //         Debug.Log("Cell ahead isn't empty");
    //         yield break;
    //     }
    //     else
    //     {
    //         yield return StepForwardOrBackwards(actor, Resources.Load<MoveData>("StepData"), true);
    //     }
    //     yield return GlobalLowLevelConcrete.Pause;
    // }

    // public static IEnumerator MoveOneCellBackwards(IConstructElement element)
    // {
    //     IActor actor = _turnProcessor.CurrentActor;
    //     if (!CellBasedCondition.CellBehindExists(actor))
    //     {
    //         yield break;
    //     }
    //     else if (!CellBasedCondition.CellBehindIsEmpty(actor))
    //     {
    //         Debug.Log("Cell ahead isn't empty");
    //         yield break;
    //     }
    //     else
    //     {
    //         yield return StepForwardOrBackwards(actor, Resources.Load<MoveData>("StepData"), false);
    //     }
    //     yield return GlobalLowLevelConcrete.Pause;
    // }


}