using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstActionEffect : BaseStatusEffect
{
    private IAction _stunned;
    public override void ChildInitialize(IActor actor)
    {
        _stunned = new BeStunned();


        if (TurnProcessorInstance == null)
            UnityEngine.Debug.Log("turnProcessor = null");

        if (LevelMasterInstance == null)
            UnityEngine.Debug.Log("levelMaster = null");

        if (LevelMasterInstance.Player == null)
            UnityEngine.Debug.Log("player = null");


        _stunned.InitializeAction(LevelMasterInstance.Player, TurnProcessorInstance, LevelMasterInstance);

        StatusConstruct = new()
        {
            new StunFirstPlayerActionStatusConcrete(TurnProcessorInstance, LevelMasterInstance, LevelMasterInstance.Player, _stunned)
        };

        DestroyAfterApplication = true;
    }
}