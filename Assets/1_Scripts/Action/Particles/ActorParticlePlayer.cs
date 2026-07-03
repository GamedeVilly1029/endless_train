using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public static class ActorParticlePlayer
{
    private static ParticleType[] _sharedParticleTypes =
    {
        ParticleType.Strike,
        ParticleType.Push,
        ParticleType.BattleCry,
        ParticleType.BasicDefend,
        ParticleType.Heal,
        ParticleType.KickBack,
        ParticleType.Stun
    };

    public static void PlayParticles(BaseActor actor, ParticleType particlType)
    {
        ActorTypeForParticle actrType = GetActorType(actor, particlType);
        string actorType = actrType.ToString();
        string particleType = particlType.ToString();

        string path = $"ParticleInfoContainers/{actorType}/{particleType}";
        string name = $"{actorType}{particleType}";
        ParticleLowLevel.ActorStartRenderParticles(actor, path, name);
    }

    public static IEnumerator PlayParticlesCoroutine(BaseActor actor, ParticleType particlType)
    {
        ActorTypeForParticle actrType = GetActorType(actor, particlType);
        string actorType = actrType.ToString();
        string particleType = particlType.ToString();

        string path = $"ParticleInfoContainers/{actorType}/{particleType}";
        string name = $"{actorType}{particleType}";
        yield return ParticleLowLevel.ActorStartRenderParticlesAndWaitTillEnd(actor, path, name);
    }

    public static void StopParticles(BaseActor actor, ParticleType particlType)
    {   
        ActorTypeForParticle actrType = GetActorType(actor, particlType);
        string actorType = actrType.ToString();
        string particleType = particlType.ToString();

        string name = $"{actorType}{particleType}";
        ParticleLowLevel.ActorStopRenderParticles(actor, name);
    }

    private static ActorTypeForParticle GetActorType(BaseActor actor, ParticleType particleType)
    {
        if (_sharedParticleTypes.Contains(particleType))
        {
            return ActorTypeForParticle.Shared;
        }
        
        if (actor is Mechanic)
        {
            return ActorTypeForParticle.Mechanic;
        }
        else if (actor is PlayerActor)
        {
           return ActorTypeForParticle.Char; 
        }
        else if (actor is Spider)
        {
            return ActorTypeForParticle.Spider;
        }
        else if (actor is ShadowBomb)
        {
            return ActorTypeForParticle.ShadowBomb;
        }
        else if (actor is ShadowCrawler)
        {
            return ActorTypeForParticle.ShadowCrawler;
        }
        else
        {
            Debug.LogError($"{actor} with the particle call: '{particleType}' operation is undefined, returning shared");
            return ActorTypeForParticle.Shared;
        }
    }
}