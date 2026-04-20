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
        Debug.Log($"{StatusConcrete} is going to be executed");
        yield return StatusConcrete(dungeonMaster, this);
    }
}