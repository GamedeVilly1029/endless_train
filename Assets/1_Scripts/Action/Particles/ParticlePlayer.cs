using UnityEngine;

public static class ParticlePlayer
{
    public static void StartBePushed(IActor actor)
    {
        if (actor is Mechanic)
        {
            StartPushingMechanic(actor as Mechanic);
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
            StopPushingMechanic(actor as Mechanic);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }
    private static void StartPushingMechanic(Mechanic mechanic)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>("ParticleInfoContainers/Mechanic/BePushed");
        ParticleSystem particles = Object.Instantiate(info.Particles, mechanic.GraphicTransform.transform);

        Debug.Log(mechanic.Transform.localScale.x);
        var main = particles.main;
        main.startSize3D = true;

        var x = main.startSizeX;
        var y = main.startSizeY;

        x.constant *= mechanic.Transform.localScale.x;
        y.constant *= mechanic.Transform.localScale.y;

        main.startSizeX = x;
        main.startSizeY = y;

        main.startRotation3D = true;
        var rotY = main.startRotationY;
        Debug.Log(main.startRotationY.mode);
        if (mechanic.IsFacingRight)
        {
            rotY.constant = 0;
        }
        else
        {
            rotY.constant = Mathf.Deg2Rad * 180f;
        }
        main.startRotationY = rotY;

        particles.gameObject.name = "MechanicBePushedParticles";
    }
        
    private static void StopPushingMechanic(Mechanic mechanic)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in mechanic.GraphicTransform.transform)
        {
            if (child.name == "MechanicBePushedParticles")
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
        // Destroyed in editor via Particle System Destroy after Stop.
    }
}