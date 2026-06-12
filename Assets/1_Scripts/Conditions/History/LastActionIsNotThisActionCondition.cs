using UnityEngine;

public class LastActionIsNotThisActionCondition : BaseConditionCommand
{
    public LastActionIsNotThisActionCondition(TurnProcessor turnProcessor, LevelMaster levelMaster)
        : base(turnProcessor, levelMaster)
    {
    }

    public override bool Execute()
    {
        if (TurnProcessorInst.CurrentActor.FightBasedActionHistory == null)
        {
            return true;
        }
        return TurnProcessorInst.CurrentActor.FightBasedActionHistory[^1].GetType() != TurnProcessorInst.CurrentAction.GetType();
    }
}