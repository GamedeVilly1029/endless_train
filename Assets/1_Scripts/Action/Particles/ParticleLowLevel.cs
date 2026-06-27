using System.Collections;
using UnityEngine;

public static class ParticleLowLevel
{
    public static void ActorStartRenderParticles(BaseActor actor, string pathToParticles, string nameToAssign)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>(pathToParticles);
        Debug.Log(pathToParticles);
        ParticleSystem particles = Object.Instantiate(info.Particles, actor.GraphicTransform.transform);

        var main = particles.main;
        ScaleParticles(ref main, actor);
        RotateIfNeeded(ref main, actor);
        RotateVelocityIfNeeded(particles, actor);

        particles.gameObject.name = nameToAssign;
    }

    public static IEnumerator ActorStartRenderParticlesAndWaitTillEnd(BaseActor actor, string pathToParticles, string nameToAssign)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>(pathToParticles);
        ParticleSystem particles = Object.Instantiate(info.Particles, actor.GraphicTransform.transform);

        float duration = particles.main.startLifetime.constantMax;

        var main = particles.main;
        ScaleParticles(ref main, actor);
        RotateIfNeeded(ref main, actor);
        RotateVelocityIfNeeded(particles, actor);

        particles.gameObject.name = nameToAssign;
        yield return new WaitForSeconds(duration);
    }

    public static void ActorStopRenderParticles(BaseActor actor, string nameToDelete)
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

    public static void CellStartRenderParticles(Cell cell, string pathToParticles, string nameToAssign)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>(pathToParticles);
        ParticleSystem particles = Object.Instantiate(info.Particles, cell.CellTransform);

        var main = particles.main;

        particles.gameObject.name = nameToAssign;
        particles.Play();
    }

    public static void CellStopRenderParticles(Cell cell, string nameToDelete)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in cell.CellTransform)
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


    private static void ScaleParticles(ref ParticleSystem.MainModule main, BaseActor actor)
    {
        main.startSize3D = true;

        var x = main.startSizeX;
        var y = main.startSizeY;

        x.constant *= actor.TransformReference.localScale.x;
        y.constant *= actor.TransformReference.localScale.y;

        main.startSizeX = x;
        main.startSizeY = y;
    }

    private static void RotateIfNeeded(ref ParticleSystem.MainModule main, BaseActor actor)
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

    private static void RotateVelocityIfNeeded(ParticleSystem particles, BaseActor actor)
    {
        if (particles.velocityOverLifetime.enabled)
        {
            var vel = particles.velocityOverLifetime;
            vel.xMultiplier *= actor.IsFacingRight() ? 1f : -1f;
        }
    }
}