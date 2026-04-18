using UnityEngine;

public static class ParticlePlayer
{
    private static string _mechanicBePushedName = "MechanicBePushed";
    private static string _charBePushedName = "CharBePushed";
    private static string _spiderBePushedName = "SpiderBePushed";

    private static string _mechanicSlideName = "MechanicSlide";
    private static string _charSlideName = "CharSlide";
    private static string _spiderSlideName = "SpiderSlide";

    private static string _StrikeName = "Strike";
    private static string _PushName = "Push";
    private static string _CryName = "Cry";

    public static void StartBePushed(IActor actor)
    {
        if (actor is Mechanic)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Mechanic/BePushed", _mechanicBePushedName);
        }
        else if (actor is PlayerActor)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Char/BePushed", _charBePushedName);
        }
        else if (actor is Spider)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Spider/BePushed", _spiderBePushedName);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }

    public static void StopBePushed(IActor actor)
    {
        if (actor is Mechanic)
        {
            ParticleLowLevel.StopRenderParticles(actor, _mechanicBePushedName);
        }
        else if (actor is PlayerActor)
        {
            ParticleLowLevel.StopRenderParticles(actor, _charBePushedName);
        }
        else if (actor is Spider)
        {
            ParticleLowLevel.StopRenderParticles(actor, _spiderBePushedName);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }

    public static void StartSlide(IActor actor)
    {
        if (actor is Mechanic)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Mechanic/BePushed", _mechanicSlideName);
        }
        else if (actor is PlayerActor)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Char/BePushed", _charSlideName);
        }
        else if (actor is Spider)
        {
            ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Spider/BePushed", _spiderSlideName);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }

    public static void StopSlide(IActor actor)
    {
        if (actor is Mechanic)
        {
            ParticleLowLevel.StopRenderParticles(actor, _mechanicSlideName);
        }
        else if (actor is PlayerActor)
        {
            ParticleLowLevel.StopRenderParticles(actor, _charSlideName);
        }
        else if (actor is Spider)
        {
            ParticleLowLevel.StopRenderParticles(actor, _spiderSlideName);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }

    public static void StartStrike(IActor actor)
    {
        ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Shared/Strike", _StrikeName);
    }

    public static void StopStrike(IActor actor)
    {
        ParticleLowLevel.StopRenderParticles(actor, _StrikeName);
    }

    public static void StartPush(IActor actor)
    {
        ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Shared/Push", _PushName);
    }

    public static void StopPush(IActor actor)
    {
        ParticleLowLevel.StopRenderParticles(actor, _PushName);
    }

    public static void StartBattleCry(IActor actor)
    {
        ParticleLowLevel.StartRenderParticles(actor, "ParticleInfoContainers/Shared/BattleCry", _CryName);
    }

    public static void StopBattleCry(IActor actor)
    {
        ParticleLowLevel.StopRenderParticles(actor, _CryName);
    }
}