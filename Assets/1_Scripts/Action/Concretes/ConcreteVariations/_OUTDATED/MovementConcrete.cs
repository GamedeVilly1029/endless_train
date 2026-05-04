using UnityEngine;
using System.Collections;

public static class MovementConcrete
{
    private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static IEnumerator WalkXTilesForward(IConstructElement element)
    // {
    //     ValueConstructElement casted = element as ValueConstructElement;
    //     int stepsToSubtract = casted.ConcreteValue;
    //     while (stepsToSubtract > 0)
    //     {
    //         Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
    //         yield return _turnProcessor.StartCoroutine(MovementLowLevelConcrete.MoveOneCellForward(element));

    //         stepsToSubtract--;
    //     }
    // }

    // public static IEnumerator WalkXTilesBackwards(IConstructElement element)
    // {
    //     ValueConstructElement casted = element as ValueConstructElement;
    //     int stepsToSubtract = casted.ConcreteValue;
    //     while (stepsToSubtract > 0)
    //     {
    //         Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
    //         yield return _turnProcessor.StartCoroutine(MovementLowLevelConcrete.MoveOneCellBackwards(element));

    //         stepsToSubtract--;
    //     }
    // }

    // public static IEnumerator RotateConcrete(IConstructElement element)
    // {
    //     yield return MovementLowLevelConcrete.RotateActor(_turnProcessor.CurrentActor);
    //     yield return GlobalLowLevelConcrete.Pause;
    // }

    // public static IEnumerator DashConcrete(IConstructElement element)
    // {
    //     ValueConstructElement casted = element as ValueConstructElement;
    //     IActor caller = element.ActionOfThisConcrete.Actor;
    //     int chargeRange = casted.ConcreteValue;

    //     if (!CellBasedCondition.CellAheadExists(caller) || !CellBasedCondition.CellAheadIsEmpty(caller))
    //     {
    //         yield break;
    //     }

    //     if (caller.IsFacingRight())
    //     {
    //         int maxCheckPos = caller.PositionCellIndex + chargeRange;
    //         for (int i = caller.PositionCellIndex + 2; i <= maxCheckPos; i++)
    //         {
    //             if (i < _levelMaster.Cells.Count)
    //             {
    //                 if (_levelMaster.Cells[i].EnityOccupyingThisCell != null)
    //                 {
    //                     yield return MovementLowLevelConcrete.Slide(caller, i - 1);
    //                     yield break;
    //                 }
    //             }
    //         }
    //     }
    //     else
    //     {
    //         int maxCheckPos = caller.PositionCellIndex - chargeRange;
    //         for (int i = caller.PositionCellIndex - 2; i >= maxCheckPos; i--)
    //         {
    //             if (i > 0)
    //             {
    //                 if (_levelMaster.Cells[i].EnityOccupyingThisCell != null)
    //                 {
    //                     yield return MovementLowLevelConcrete.Slide(caller, i + 1);
    //                     yield break;
    //                 }
    //             }
    //         }
    //     }

    //     yield return MovementLowLevelConcrete.StepForwardOrBackwards(caller, Resources.Load<MoveData>("StepData"), true);
    //     yield return GlobalLowLevelConcrete.Pause;
    // }
}