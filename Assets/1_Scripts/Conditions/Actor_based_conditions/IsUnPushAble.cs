using UnityEngine;

public class IsUnPushAble : BaseConditionCommand
{
    private BaseActor _actorToCheck;
    public IsUnPushAble(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actorToCheck) : base(turnProcessor, levelMaster)
    {
        _actorToCheck = actorToCheck;
    }

    // public override bool Execute()
    // {
    //     // foreach (ActorTrait _actorToCheck.Traits)
    // }
}
