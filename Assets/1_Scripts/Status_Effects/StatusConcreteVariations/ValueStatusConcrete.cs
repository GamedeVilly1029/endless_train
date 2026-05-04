using System.Collections;
using UnityEngine;
public class ValueStatusConcrete : BaseStatusConcrete
{
    public int Value;

    public ValueStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        IActor actor,
        int value
    ):base(turnProcessor, levelMaster, actor) 
    {
        Value = value;
    }

    public override IEnumerator ChildExecute()
    {
        Debug.LogWarning("Base version of ValueStatusConcrete was called. Implement proper status");
        yield break;
    }
}