using UnityEngine;

public static class ParticleLowLevel
{
    public static void StartRenderParticles(IActor actor, string pathToParticles, string nameToAssign)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>(pathToParticles);
        ParticleSystem particles = Object.Instantiate(info.Particles, actor.GraphicTransform.transform);

        var main = particles.main;
        ScaleParticles(ref main, actor);
        RotateIfNeeded(ref main, actor);
        RotateVelocityIfNeeded(particles, actor);

        particles.gameObject.name = nameToAssign;
    }

    public static void StopRenderParticles(IActor actor, string nameToDelete)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in actor.GraphicTransform.transform)
        {
            if (child.name == nameToDelete)
            {
                transformWithParticles = child;
                break;
            }
        }

        if (transformWithParticles == null)
        {
            Debug.LogError("There's no particle system for this method to stop");
            return;
        }

        ParticleSystem particles = transformWithParticles.GetComponent<ParticleSystem>();
        particles.Stop(); 
    }

    private static void ScaleParticles(ref ParticleSystem.MainModule main, IActor actor)
    {
        main.startSize3D = true;

        var x = main.startSizeX;
        var y = main.startSizeY;

        x.constant *= actor.TransformReference.localScale.x;
        y.constant *= actor.TransformReference.localScale.y;

        main.startSizeX = x;
        main.startSizeY = y;
    }

    private static void RotateIfNeeded(ref ParticleSystem.MainModule main, IActor actor)
    {
        main.startRotation3D = true;
        var rotY = main.startRotationY;

        if (actor.IsFacingRight())
        {
            rotY.constant = 0;
        }
        else
        {
            rotY.constant = Mathf.Deg2Rad * 180f;
        }
        main.startRotationY = rotY;
    }

    private static void RotateVelocityIfNeeded(ParticleSystem particles, IActor actor)
    {
        if (particles.velocityOverLifetime.enabled)
        {
            var vel = particles.velocityOverLifetime;
            vel.xMultiplier *= actor.IsFacingRight() ? 1f : -1f;
        }
    }
}