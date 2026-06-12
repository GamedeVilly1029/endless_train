using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstActionEffect : BaseStatusEffect
{
    private BaseAction _stunned;
    public override void ChildInitialize(BaseActor actor)
    {
        _stunned = new BeStunned();
        _stunned.Initialize(LevelMasterInstance.Player, TurnProcessorInstance, LevelMasterInstance, 0, "SkillActionUI/Stunned");

        StatusConstruct = new()
        {
            new StunFirstPlayerActionStatusConcrete(TurnProcessorInstance, LevelMasterInstance, LevelMasterInstance.Player, _stunned)
        };

        DestroyAfterApplication = true;
    }
}