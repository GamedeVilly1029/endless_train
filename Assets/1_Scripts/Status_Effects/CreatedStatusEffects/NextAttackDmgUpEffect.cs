using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAttackDmgUpEffect : BaseStatusEffect 
{
    public override void ChildInitialize(BaseActor actor)
    {
        StatusConstruct = new()
        {
            new IncreaseDamageOfFirstAttackStatusConcrete(TurnProcessorInstance, LevelMasterInstance, Actor, 2)
        };

        DestroyAfterApplication = true;
    }
}