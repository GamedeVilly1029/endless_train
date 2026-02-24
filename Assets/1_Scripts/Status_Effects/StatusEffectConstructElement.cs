using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectConstructElement
{
    public Func<DungeonMaster, StatusEffectConstructElement, IEnumerator> StatusConcrete;
    public int StatusConcreteValue;

    public StatusEffectConstructElement(Func<DungeonMaster, StatusEffectConstructElement, IEnumerator> concrete, int value)
    {
        StatusConcrete = concrete;
        StatusConcreteValue = value;
    }

    public IEnumerator ExecuteStatusConcrete(DungeonMaster dungeonMaster)
    {
        yield return StatusConcrete(dungeonMaster, this);
    }
}