using UnityEngine;

public class ActorInRangeOfCellsCondition : BaseConditionCommand
{
    private IActor _actor;
    private int _start;
    private int _end;

    public ActorInRangeOfCellsCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor, int start, int end)
        : base(turnProcessor, levelMaster)
    {
        _actor = actor;
        _start = start;
        _end = end;
    }

    public override bool Execute()
    {
        for (int i = _start; i <= _end; i++)
        {
            if (_actor.PositionCellIndex == i)
            {
                return true;
            }
        }
        return false;
    }
}