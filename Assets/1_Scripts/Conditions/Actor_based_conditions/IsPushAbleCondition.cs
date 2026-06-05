using UnityEngine;

public class IsPushAbleCondition : BaseConditionCommand
{
    private BaseActor _actorToCheck;
    public IsPushAbleCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actorToCheck) : base(turnProcessor, levelMaster)
    {
        _actorToCheck = actorToCheck;
    }

    public override bool Execute()
    {
        foreach (ActorTrait trait in _actorToCheck.Traits)
        {
            if (trait is UnPushAbleTrait)
            {
                return false;
            }
        }
        return true;
    }
}
