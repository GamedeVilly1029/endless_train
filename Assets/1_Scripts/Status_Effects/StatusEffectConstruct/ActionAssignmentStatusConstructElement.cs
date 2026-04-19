using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAssignmentStatusConstructElement : BaseStatusEffectConstructElement
{
    public IAction ActionToAssign;

    public ActionAssignmentStatusConstructElement(
    Func<DungeonMaster, IStatusEffectConstructElement, IEnumerator> concrete,
    IAction actionToAssign,
    IActor actor):base(concrete, actor) 
    {
        ActionToAssign = actionToAssign;
    }
}