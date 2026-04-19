using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValueStatusEffectConstructElement : BaseStatusEffectConstructElement
{
    public int StatusConcreteValue;

    public ValueStatusEffectConstructElement(
    Func<DungeonMaster, IStatusEffectConstructElement, IEnumerator> concrete,
    int value,
    IActor actor):base(concrete, actor) 
    {
        StatusConcreteValue = value;
    }
}