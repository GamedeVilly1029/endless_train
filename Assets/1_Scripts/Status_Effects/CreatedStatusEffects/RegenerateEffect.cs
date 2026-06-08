using UnityEngine;

public class RegenerateEffect : BaseStatusEffect
{
    public override void ChildInitialize(BaseActor actor)
    {
        StatusConstruct = new()
        {
            new RegenerateStatusConcrete(TurnProcessorInstance, LevelMasterInstance, Actor, 5)
        };

        DestroyAfterApplication = false;
    }
}