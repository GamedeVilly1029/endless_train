using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAddStatusConcrete : BaseStatusConcrete
{
    public BaseAction ActionToAssign;

    public ActionAddStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        BaseActor actor,
        BaseAction actionToAssign
    ):base(turnProcessor, levelMaster, actor) 
    {
        ActionToAssign = actionToAssign;
    }
}