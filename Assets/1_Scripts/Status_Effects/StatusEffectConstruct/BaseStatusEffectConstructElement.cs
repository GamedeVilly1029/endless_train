using System;
using System.Collections;
using UnityEngine;

public class BaseStatusEffectConstructElement : IStatusEffectConstructElement
{
    public Func<DungeonMaster, IStatusEffectConstructElement, IEnumerator> StatusConcrete{get;set;}
    public IActor Actor{get;set;}

    public BaseStatusEffectConstructElement(Func<DungeonMaster, IStatusEffectConstructElement, IEnumerator> concrete, IActor actor)
    {
        StatusConcrete = concrete;
        Actor = actor;
    }

    public IEnumerator ExecuteStatusConcrete(DungeonMaster dungeonMaster)
    {
        yield return StatusConcrete(dungeonMaster, this);
    }
}