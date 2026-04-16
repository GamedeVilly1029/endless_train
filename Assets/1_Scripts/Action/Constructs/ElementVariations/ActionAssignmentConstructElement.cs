using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class ActionAssignmentConstructElement : BaseConstructElement
{
    public IAction ActionToAssign;
    public ActionAssignmentConstructElement
    (
    IAction actionOfThisConcrete,
    List<Func<DungeonMaster, IActor, bool>> conditions, 
    Func<DungeonMaster, IConstructElement, IEnumerator> concrete, 
    ActionConcreteTag tag,
    IAction actionToAssign 
    ):base(actionOfThisConcrete, conditions, concrete, tag)
    {
        ActionToAssign = actionToAssign;
    }

    public override IConstructElement Clone(IAction actionClone)
    {
        ActionAssignmentConstructElement clone = new(
           actionClone,
           ConditionsToExecuteConcrete,
           Concrete,
           ConcreteTag,
           ActionToAssign
        );
        return clone;
    }
}