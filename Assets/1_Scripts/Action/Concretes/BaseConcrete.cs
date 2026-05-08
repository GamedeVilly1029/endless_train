using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConcrete : IConcrete
{
    public TurnProcessor TurnProcessorInst;
    public LevelMaster LevelMasterInst;
    public IAction ActionOfThisConcrete;
    public List<IConditionCommand> ExtraConditions;
    public ActionConcreteTag Tag;

    public BaseConcrete(
    TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag
    )
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;
        ActionOfThisConcrete = actionOfThisConcrete;
        ExtraConditions = extraConditions;
        Tag = tag;
    }

    public IEnumerator Execute()
    {
        if (ExtraConditions == null)
        {
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(this);
            yield return ChildExecute();
        }
        else
        {
            foreach (var condition in ExtraConditions)
            {
                if (!condition.Execute())
                {
                    yield break;
                }
            }
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(this);
            yield return ChildExecute();
        }
    }

    public virtual IEnumerator ChildExecute()
    {
        Debug.LogWarning("Base version of Concrete's Execute was called. Implement this concrete properly");
        yield break;
    }

    public virtual IConcrete Clone(IAction clonedAction)
    {
        Debug.LogWarning($"Implement Clonning of this concrete");
        return null;
    }
}
