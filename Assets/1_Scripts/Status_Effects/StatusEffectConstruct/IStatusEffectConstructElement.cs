using System;
using System.Collections;
using UnityEngine;


public interface IStatusEffectConstructElement
{
    public Func<DungeonMaster, IStatusEffectConstructElement, IEnumerator> StatusConcrete{get;set;}
    public IActor Actor{get;set;}

    public IEnumerator ExecuteStatusConcrete(DungeonMaster dungeonMaster);
}