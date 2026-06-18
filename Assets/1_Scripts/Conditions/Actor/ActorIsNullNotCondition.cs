using UnityEngine;

public class ActorIsNotNullCondition : BaseConditionCommand
{
    private BaseActor _actor;
    public ActorIsNotNullCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actor) : base(turnProcessor, levelMaster)
    {
        _actor = actor;
    }

    public override bool Execute()
    {
        return _actor != null;
    }
}