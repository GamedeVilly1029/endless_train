using UnityEngine;

public class TurnActionHistoryIsEmpty : BaseConditionCommand
{
    private BaseActor _toCheck;

    public TurnActionHistoryIsEmpty(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor toCheck) : base(turnProcessor, levelMaster)
    {
        _toCheck = toCheck;
    }

    public override bool Execute()
    {
        return _toCheck.TurnBasedActionHistory.Count == 0;
    }
}