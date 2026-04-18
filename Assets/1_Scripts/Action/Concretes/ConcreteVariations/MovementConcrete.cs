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
            yield return dungeonMaster.StartCoroutine(LowLevelConcrete.MoveOneCellForward(dungeonMaster, element));

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
            yield return dungeonMaster.StartCoroutine(LowLevelConcrete.MoveOneCellBackwards(dungeonMaster, element));

            stepsToSubtract--;
        }
    }
    public static IEnumerator RotateConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return ActorPosManipulation.RotateActor(dungeonMaster.CurrentActor);
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator DashConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        IActor caller = element.ActionOfThisConcrete.Actor;
        int chargeRange = casted.ConcreteValue; 

        if (caller.IsFacingRight())
        {
            int maxCheckPos = caller.PositionCellIndex + chargeRange;

            if (!CellBasedCondition.CellAheadExists(dungeonMaster, caller) || !CellBasedCondition.CellAheadIsEmpty(dungeonMaster, caller))
            {
                Debug.Log("Cell to the right from the caller is either not exists or occupied => can't move there");
                yield break;
            }

            for(int i = caller.PositionCellIndex + 2; i <= maxCheckPos; i++)
            {
                if (i < dungeonMaster.Cells.Count)
                {
                    if (dungeonMaster.Cells[i].EnityOccupyingThisCell != null)
                    {
                        yield return ActorPosManipulation.Slide(dungeonMaster, caller, i - 1);
                        yield break;
                    }
                }
            }
        }
        else
        {

            if (!CellBasedCondition.CellAheadExists(dungeonMaster, caller) || !CellBasedCondition.CellAheadIsEmpty(dungeonMaster, caller))
            {
                Debug.Log("Cell to the left from the caller is either not exists or occupied => can't move there");
                yield break;
            }

            int maxCheckPos = caller.PositionCellIndex - chargeRange;
            for(int i = caller.PositionCellIndex - 2; i >= maxCheckPos; i--)
            {
                if (i > 0)
                {
                    if (dungeonMaster.Cells[i].EnityOccupyingThisCell != null)
                    {
                        // Debug.Log("Concrete is called");
                        yield return ActorPosManipulation.Slide(dungeonMaster, caller, i + 1);
                        yield break;
                    }
                }
            }
        }

        yield return ActorPosManipulation.StepForwardOrBackwards(dungeonMaster, caller, Resources.Load<MoveData>("StepData"), true);
    }
}