using System.Collections;
using UnityEngine;

public class ParticleActorExplodePlayer : GraphicalCoroutineConcrete
{
    private BaseActor _actor;

    public ParticleActorExplodePlayer(BaseActor actor)
    {
        _actor = actor;
    }

    public override IEnumerator Execute()
    {
        yield return ActorParticlePlayer.PlayParticlesCoroutine(_actor, ParticleType.Explode);
    }
} 