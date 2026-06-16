using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSummonDMGIncreaseConcrete : BaseConcrete
{
    private Summoner _summoner;
    
    public AllSummonDMGIncreaseConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag,
        Summoner summoner
        ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
        _summoner = summoner;
    }

    public override IEnumerator ChildExecute()
    {
        foreach (Summon summon in _summoner.Summons)
        {
            yield return new SummonDMGIncreaseConcrete(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, ActionConcreteTag.Skill, _summoner, summon);
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new AllSummonDMGIncreaseConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, _summoner);
    }
}