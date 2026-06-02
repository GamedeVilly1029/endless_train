using UnityEngine;

public class ConcreteHistoryIsEmptyCondition : BaseConditionCommand
{
    public ConcreteHistoryIsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster)
        : base(turnProcessor, levelMaster)
    {
    }

    public override bool Execute()
    {
        return TurnProcessorInst.CurrentAction.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    }
}