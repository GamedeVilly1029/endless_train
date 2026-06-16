using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConcrete : IConcrete
{
    public TurnProcessor TurnProcessorInst;
    public LevelMaster LevelMasterInst;
    public BaseAction ActionOfThisConcrete;
    public List<IConditionCommand> ExtraConditions;
    public List<IConditionCommand> ActionPassedConditions;
    public ActionConcreteTag Tag;

    public BaseConcrete(
    TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    BaseAction actionOfThisConcrete, 
    List<IConditionCommand> actionPassedConditions, 
    ActionConcreteTag tag
    )
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;
        ActionOfThisConcrete = actionOfThisConcrete;
        ActionPassedConditions = actionPassedConditions;
        Tag = tag;
    }

    public IEnumerator Execute()
    {
        ConditionCalc();
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
                    Debug.Log($"Violates: {condition}");
                    yield return DeclinedConcrete();
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

    public virtual IConcrete Clone(BaseAction clonedAction)
    {
        Debug.LogWarning($"Implement Cloning of this concrete");
        return null;
    }

    public virtual IEnumerator DeclinedConcrete()
    {
        yield break;
    }

    private void ConditionCalc()
    {
        ExtraConditions = new();
        List<IConditionCommand> builtInConditions = CreateBaseConditionList();
        AddToExtraConditions(builtInConditions);
        AddToExtraConditions(ActionPassedConditions);
    }

    public virtual List<IConditionCommand> CreateBaseConditionList()
    {
        // Base conditions of the Concrete should be created and initialized there
        return null;
    }

    private void AddToExtraConditions(List<IConditionCommand> conditions)
    {
        if (conditions != null && conditions.Count > 0)
        {
            foreach (IConditionCommand condition in conditions)
            {
                ExtraConditions.Add(condition);
            }
        }
    }
}