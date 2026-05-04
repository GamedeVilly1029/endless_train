using UnityEngine;

public class PositionIndexChangedInPreviousActionCondition : BaseConditionCommand
{
    private IActor _actor;

    public PositionIndexChangedInPreviousActionCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        if (_actor.PositionCellIndexHistory.Count >= 2)
        {
            int lastIndex = _actor.PositionCellIndexHistory.Pop();
            int secondToLastIndex = _actor.PositionCellIndexHistory.Peek();
            _actor.PositionCellIndexHistory.Push(lastIndex);

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
}