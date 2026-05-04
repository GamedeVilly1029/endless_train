using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    TurnProcessor TurnProcessorInstance{get;set;}
    LevelMaster LevelMasterInstance{get;set;}
    IActor Actor{get;set;}
    List<IStatusConcrete> StatusConstruct{get;set;}
    bool DestroyAfterApplication{get;set;}

    void ChildInitializeStatusEffect(IActor actor);
    void InitializeStatusEffect(TurnProcessor turnProcessor,LevelMaster levelMaster, IActor actor);
    IEnumerator ApplyStatusEffect();
    void SelfDestroy(List<IStatusEffect> listToRemoveFrom);
}