using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class ValueConstructElement : BaseConstructElement
{
    public int ConcreteValue;
    public ValueConstructElement
    (
    IAction actionOfThisConcrete,
    List<Func<DungeonMaster, IActor, bool>> conditions, 
    Func<DungeonMaster, IConstructElement, IEnumerator> concrete, 
    ActionConcreteTag tag,
    int concreteValue
    ):base(actionOfThisConcrete, conditions, concrete, tag)
    {
        ConcreteValue = concreteValue;
    }

    public override IConstructElement Clone(IAction actionClone)
    {
        ValueConstructElement clone = new(
           actionClone,
           ConditionsToExecuteConcrete,
           Concrete,
           ConcreteTag,
           ConcreteValue
        );
        return clone;
    }
}