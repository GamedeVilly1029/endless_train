using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstActionEffect : BaseStatusEffect
{
    private IAction _stunned;
    public override void ChildInitializeStatusEffect(IActor actor)
    {
        _stunned = new BeStunned();
        _stunned.InitializeAction(LevelMasterInstance.Player, TurnProcessorInstance, LevelMasterInstance);

        StatusConstruct = new()
        {
            new StunFirstPlayerActionStatusConcrete(TurnProcessorInstance, LevelMasterInstance, LevelMasterInstance.Player, _stunned)
        };

        DestroyAfterApplication = true;
    }
}