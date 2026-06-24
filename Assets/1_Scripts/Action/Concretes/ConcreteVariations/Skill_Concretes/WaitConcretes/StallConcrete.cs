using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallConcrete : BaseConcrete
{
    public StallConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        Debug.Log("Stall...");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new StallConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag);
    }
}
