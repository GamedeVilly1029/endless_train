using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusEffect : IStatusEffect
{
    public List<IStatusConcrete> StatusConstruct { get; set; }
    public bool DestroyAfterApplication { get; set; }
    public TurnProcessor TurnProcessorInstance { get; set; }
    public LevelMaster LevelMasterInstance { get; set; }
    public IActor Actor { get; set; }

    public virtual void ChildInitialize(IActor actor)
    {
        Debug.LogWarning("BaseStatusEffect initialization was called!");
    }

    public void Initialize(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
    {
        BaseInitialization(turnProcessor, levelMaster, actor);
        Debug.Log("Base initialization of the turnProcessor and levelMaster are done");
        ChildInitialize(actor);
    }

    private void BaseInitialization(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actor)
    {
        TurnProcessorInstance = turnProcessor;
        LevelMasterInstance = levelMaster;
        Actor = actor;
    }

    public IEnumerator Apply()
    {
        foreach (IStatusConcrete element in StatusConstruct)
        {
            yield return element.Execute();
        }
    }

    public void SelfDestroy(List<IStatusEffect> listToRemoveFrom)
    {
        listToRemoveFrom.Remove(this);
    }
}