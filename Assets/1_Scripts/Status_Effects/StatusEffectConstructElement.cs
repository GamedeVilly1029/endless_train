using System;
using System.Collections;
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
        Debug.Log("Status effect concrete is executed");
        yield return StatusConcrete(dungeonMaster, this);
    }
}