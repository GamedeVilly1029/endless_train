using UnityEngine;

public class ConcreteHistoryIsEmptyCondition : BaseConditionCommand
{
    private BaseAction _action;
    public ConcreteHistoryIsEmptyCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseAction action)
        : base(turnProcessor, levelMaster)
    {
        _action = action;
    }

    public override bool Execute()
    {
        Debug.Log(_action.TurnTemporarySuccessfulConcreteHistory.Count == 0);
        return _action.TurnTemporarySuccessfulConcreteHistory.Count == 0;
    }
}