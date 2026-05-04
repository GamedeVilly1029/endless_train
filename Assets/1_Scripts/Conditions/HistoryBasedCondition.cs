using UnityEngine;

public static class HistoryBasedCondition
{
    // private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    // private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static bool PositionIndexChangedInPreviousAction(IActor actor)
    // {
    //     if (actor.PositionCellIndexHistory.Count >= 2)
    //     {
    //         int lastIndex = actor.PositionCellIndexHistory.Pop();
    //         int secondToLastIndex = actor.PositionCellIndexHistory.Peek();
    //         actor.PositionCellIndexHistory.Push(lastIndex);

    //         if (secondToLastIndex != lastIndex)
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public static bool ConcreteHistoryIsEmpty()
    // {
    //     return _turnProcessor.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    // }

    // public static bool LastActionIsNotThisAction()
    // {
    //     if (_turnProcessor.CurrentActor.FightBasedActionHistory == null)
    //     {
    //         return true;
    //     }
    //     return _turnProcessor.CurrentActor.FightBasedActionHistory[^1].GetType() != _turnProcessor.CurrentAction.GetType();
    // }

    // public static bool ConcreteHistoryHasOnly1Concrete(TurnProcessor turnProcessor)
    // {
    //     return turnProcessor.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 1;
    // }
}