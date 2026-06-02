using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    TurnProcessor TurnProcessorInstance{get;set;}
    LevelMaster LevelMasterInstance{get;set;}
    BaseActor Actor{get;set;}
    List<IStatusConcrete> StatusConstruct{get;set;}
    bool DestroyAfterApplication{get;set;}

    void ChildInitialize(BaseActor actor);
    void Initialize(TurnProcessor turnProcessor,LevelMaster levelMaster, BaseActor actor);
    IEnumerator Apply();
    void SelfDestroy(List<IStatusEffect> listToRemoveFrom);
}