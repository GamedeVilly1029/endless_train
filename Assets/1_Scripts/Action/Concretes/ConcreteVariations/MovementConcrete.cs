using UnityEngine;
using System.Collections;

public static class MovementConcrete
{
    public static IEnumerator WalkXTilesForward(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        int stepsToSubtract = casted.ConcreteValue;
        while (stepsToSubtract > 0)
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return dungeonMaster.StartCoroutine(MovementLowLevelConcrete.MoveOneCellForward(dungeonMaster, element));

            stepsToSubtract--;
        }
    }

    public static IEnumerator WalkXTilesBackwards(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        int stepsToSubtract = casted.ConcreteValue;
        while (stepsToSubtract > 0)
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return dungeonMaster.StartCoroutine(MovementLowLevelConcrete.MoveOneCellBackwards(dungeonMaster, element));

            stepsToSubtract--;
        }
    }

    public static IEnumerator RotateConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return MovementLowLevelConcrete.RotateActor(dungeonMaster.CurrentActor);
        yield return GlobalLowLevelConcrete.Pause;
    }

    public static IEnumerator DashConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        IActor caller = element.ActionOfThisConcrete.Actor;
        int chargeRange = casted.ConcreteValue;

        if (!CellBasedCondition.CellAheadExists(dungeonMaster, caller) || !CellBasedCondition.CellAheadIsEmpty(dungeonMaster, caller))
        {
            Debug.Log("Next cell alongside caller's rotation is either not exists or occupied => can't move there");
            yield break;
        }

        if (caller.IsFacingRight())
        {
            int maxCheckPos = caller.PositionCellIndex + chargeRange;
            for (int i = caller.PositionCellIndex + 2; i <= maxCheckPos; i++)
            {
                if (i < dungeonMaster.Cells.Count)
                {
                    if (dungeonMaster.Cells[i].EnityOccupyingThisCell != null)
                    {
                        yield return MovementLowLevelConcrete.Slide(dungeonMaster, caller, i - 1);
                        yield break;
                    }
                }
            }
        }
        else
        {
            int maxCheckPos = caller.PositionCellIndex - chargeRange;
            for (int i = caller.PositionCellIndex - 2; i >= maxCheckPos; i--)
            {
                if (i > 0)
                {
                    if (dungeonMaster.Cells[i].EnityOccupyingThisCell != null)
                    {
                        yield return MovementLowLevelConcrete.Slide(dungeonMaster, caller, i + 1);
                        yield break;
                    }
                }
            }
        }

        yield return MovementLowLevelConcrete.StepForwardOrBackwards(dungeonMaster, caller, Resources.Load<MoveData>("StepData"), true);
    }
}