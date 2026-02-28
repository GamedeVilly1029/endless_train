using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectConstructElement
{
    public Func<DungeonMaster, StatusEffectConstructElement, IEnumerator> StatusConcrete;
    public int StatusConcreteValue;
    public IActor Actor;

    public StatusEffectConstructElement(Func<DungeonMaster, StatusEffectConstructElement, IEnumerator> concrete, int value, IActor actor)
    {
        StatusConcrete = concrete;
        StatusConcreteValue = value;
        Actor = actor;
    }

    public IEnumerator ExecuteStatusConcrete(DungeonMaster dungeonMaster)
    {
        yield return StatusConcrete(dungeonMaster, this);
    }
}