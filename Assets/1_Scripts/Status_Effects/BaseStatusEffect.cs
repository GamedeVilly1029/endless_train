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

    public virtual void ChildInitializeStatusEffect(IActor actor)
    {
        Debug.LogWarning("BaseStatusEffect initialization was called!");
    }

    public void InitializeStatusEffect(TurnProcessor dungeonMaster, LevelMaster levelMaster, IActor actor)
    {
        BaseInitialization(dungeonMaster, levelMaster, actor);
        ChildInitializeStatusEffect(actor);
    }

    private void BaseInitialization(TurnProcessor dungeonMaster, LevelMaster levelMaster, IActor actor)
    {
        TurnProcessorInstance = dungeonMaster;
        LevelMasterInstance = levelMaster;
        Actor = actor;
    }

    public IEnumerator ApplyStatusEffect()
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