using UnityEngine;

public static class HistoryBasedCondition
{
    public static bool PositionIndexChangedInPreviousAction(DungeonMaster master, IActor actor)
    {
        if (actor.PositionCellIndexHistory.Count >= 2)
        {
            int lastIndex = actor.PositionCellIndexHistory.Pop();
            int secondToLastIndex = actor.PositionCellIndexHistory.Peek();
            actor.PositionCellIndexHistory.Push(lastIndex);

            if (secondToLastIndex != lastIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static bool ConcreteHistoryIsEmpty(DungeonMaster dungeonMaster, IActor actor)
    {
        return dungeonMaster.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    }

    public static bool LastActionIsNotThisAction(DungeonMaster dungeonMaster, IActor actor)
    {
        if (dungeonMaster.CurrentActor.FightBasedActionHistory == null)
        {
            return true;
        }
        return dungeonMaster.CurrentActor.FightBasedActionHistory[^1].GetType() != dungeonMaster.CurrentAction.GetType();
    }

    public static bool ConcreteHistoryHasOnly1Concrete(DungeonMaster dungeonMaster, IActor actor)
    {
        return dungeonMaster.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 1;
    }
}