using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    List<StatusEffectConstructElement> StatusConstruct{get;set;}
    bool DestroyAfterApplication{get;set;}

    void InitializeStatusEffect(DungeonMaster dungeonMaster);
    IEnumerator ApplyStatusEffect(DungeonMaster dungeonMaster);
    void SelfDestroy(DungeonMaster dungeonMaster);
}