using UnityEngine;

public class LastActionIsNotThisActionCondition : BaseConditionCommand
{
    public LastActionIsNotThisActionCondition(TurnProcessor turnProcessor, LevelMaster levelMaster)
        : base(turnProcessor, levelMaster)
    {
    }

    public override bool Execute()
    {
        if (TurnProcessor.CurrentActor.FightBasedActionHistory == null)
        {
            return true;
        }
        return TurnProcessor.CurrentActor.FightBasedActionHistory[^1].GetType() != TurnProcessor.CurrentAction.GetType();
    }
}