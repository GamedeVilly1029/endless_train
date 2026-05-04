using System;
using System.Collections;
using UnityEngine;

public class BaseStatusConcrete : IStatusConcrete
{
    public TurnProcessor TurnProcessorInst;
    public LevelMaster LevelMasterInst;
    public IActor Actor;

    public BaseStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        IActor actor
    )
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;
        Actor = actor;
    }

    public IEnumerator Execute()
    {
        yield return ChildExecute();
    }

    public virtual IEnumerator ChildExecute()
    {
        Debug.LogWarning("Base version of BaseStatusConcrete was called. Implement proper status");
        yield break;
    }
}