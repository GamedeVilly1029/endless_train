using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAddStatusConcrete : BaseStatusConcrete
{
    public IAction ActionToAssign;

    public ActionAddStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        IActor actor,
        IAction actionToAssign
    ):base(turnProcessor, levelMaster, actor) 
    {
        ActionToAssign = actionToAssign;
    }
}