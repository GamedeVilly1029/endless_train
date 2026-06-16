using UnityEngine;

public class IsSummonCondition : BaseConditionCommand
{
    private BaseActor _toCheck;
    public IsSummonCondition
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        BaseActor toCheck
    )
    :base(turnProcessor, levelMaster)
    {
        _toCheck = toCheck;
    }

    public override bool Execute()
    {
        if (_toCheck is Summon)
        {
            return true;
        }
        return false;
    }
}
