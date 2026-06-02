using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHealParticlesGraphicConcrete : GraphicalConcrete
{
    BaseActor _caster;
    public PlayHealParticlesGraphicConcrete
    (
        BaseActor caster
    )
    {
        _caster = caster;
    }

    public override void Execute()
    {
        ActorParticlePlayer.PlayParticles(_caster, ParticleType.Heal);
    }
}
