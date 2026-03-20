using UnityEngine;

public static class ParticlePlayer
{
    public static void StartBePushed(IActor actor)
    {
        if (actor is Mechanic)
        {
            StartPushingMechanic(actor as Mechanic);
        }
        else if (actor is PlayerActor)
        {
            StartPushingChar(actor as PlayerActor);
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
        else if (actor is PlayerActor)
        {
            StopPushingChar(actor as PlayerActor);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }

    public static void StartStrike(IActor actor)
    {
        if (actor is Mechanic)
        {
            StartStrikeMechanic(actor as Mechanic);
        }
        else if (actor is PlayerActor)
        {
            StartStrikeChar(actor as PlayerActor);
        }
        else
        {
            Debug.LogWarning("Not Implemented for other characters");
        }
    }
    public static void StopStrike(IActor actor)
    {
        if (actor is Mechanic)
        {
            StopStrikeMechanic(actor as Mechanic);
        }
        else if (actor is PlayerActor)
        {
            StopStrikeChar(actor as PlayerActor);
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
    }


    private static void StartPushingChar(PlayerActor player)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>("ParticleInfoContainers/Char/BePushed");
        ParticleSystem particles = Object.Instantiate(info.Particles, player.GraphicTransform.transform);

        var main = particles.main;
        main.startSize3D = true;

        var x = main.startSizeX;
        var y = main.startSizeY;

        x.constant *= player.Transform.localScale.x;
        y.constant *= player.Transform.localScale.y;

        main.startSizeX = x;
        main.startSizeY = y;

        main.startRotation3D = true;
        var rotY = main.startRotationY;
        if (player.IsFacingRight)
        {
            rotY.constant = 0;
        }
        else
        {
            rotY.constant = Mathf.Deg2Rad * 180f;
        }
        main.startRotationY = rotY;

        particles.gameObject.name = "CharBePushedParticles";
    }
        
    private static void StopPushingChar(PlayerActor player)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in player.GraphicTransform.transform)
        {
            if (child.name == "CharBePushedParticles")
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

    private static void StartStrikeChar(PlayerActor player)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>("ParticleInfoContainers/Char/Strike");
        ParticleSystem particles = Object.Instantiate(info.Particles, player.GraphicTransform.transform);

        var main = particles.main;
        main.startSize3D = true;

        var x = main.startSizeX;
        var y = main.startSizeY;

        x.constant *= player.Transform.localScale.x;
        y.constant *= player.Transform.localScale.y;

        main.startSizeX = x;
        main.startSizeY = y;

        main.startRotation3D = true;
        var rotY = main.startRotationY;
        if (player.IsFacingRight)
        {
            rotY.constant = 0;
        }
        else
        {
            rotY.constant = Mathf.Deg2Rad * 180f;
        }
        main.startRotationY = rotY;
        particles.gameObject.name = "CharStrikeParticles";
    }

    private static void StopStrikeChar(PlayerActor player)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in player.GraphicTransform.transform)
        {
            if (child.name == "CharStrikeParticles")
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

        private static void StartStrikeMechanic(Mechanic mechanic)
    {
        ParticleInfoContainer info = Resources.Load<ParticleInfoContainer>("ParticleInfoContainers/Mechanic/Strike");
        ParticleSystem particles = Object.Instantiate(info.Particles, mechanic.GraphicTransform.transform);

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
        if (mechanic.IsFacingRight)
        {
            rotY.constant = 0;
        }
        else
        {
            rotY.constant = Mathf.Deg2Rad * 180f;
        }
        main.startRotationY = rotY;
        particles.gameObject.name = "MechanicStrikeParticles";
    }

    private static void StopStrikeMechanic(Mechanic mechanic)
    {
        Transform transformWithParticles = null;
        foreach (Transform child in mechanic.GraphicTransform.transform)
        {
            if (child.name == "MechanicStrikeParticles")
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
}