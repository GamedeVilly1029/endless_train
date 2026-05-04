using UnityEngine;

public class ConcreteHistoryHasOnly1ConcreteCondition : BaseConditionCommand
{
    public ConcreteHistoryHasOnly1ConcreteCondition(TurnProcessor turnProcessor, LevelMaster levelMaster)
        : base(turnProcessor, levelMaster)
    {

    }

    public override bool Execute()
    {
        return TurnProcessor.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 1;
    }
}