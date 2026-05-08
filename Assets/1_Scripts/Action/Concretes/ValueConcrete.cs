using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueConcrete : BaseConcrete
{
    public int Value;
    public ValueConcrete(
    TurnProcessor turnProcessor, 
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    int value
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        Value = value;
    }

    public override IEnumerator ChildExecute()
    {
        Debug.LogWarning("ValueConcrete's base Execute method version was called. Implement proper ValueConcrete");
        yield break;
    }
}
