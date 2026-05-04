using UnityEngine;

public class ConcreteHistoryIsEmptyCondition : BaseConditionCommand
{
    public ConcreteHistoryIsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster)
        : base(turnProcessor, levelMaster)
    {
    }

    public override bool Execute()
    {
        return TurnProcessor.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    }
}