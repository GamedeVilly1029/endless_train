using System.Collections;
using UnityEngine;

public class RegenerateStatusConcrete : ValueStatusConcrete
{
    public RegenerateStatusConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseActor actor, 
        int value
    ):base(turnProcessor, levelMaster, actor, value)
    {
    }

    public override IEnumerator ChildExecute()
    {
        Actor.CurrentHP += Value;
        new GraphicTransformColorLerpConcrete(Actor, Color.green, 0.5f).Execute();
        new PlayHealParticlesGraphicConcrete(Actor).Execute();
        yield return null;
    }
}
