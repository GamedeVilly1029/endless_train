using UnityEngine;

public static class CellParticlePlayer
{
    private static string _nameLineStart = "ShockWave";
    private static string _nameLineEnd = "ParticleSystemObject";

    public static void StartWave(Cell cell, WaveDirection dir, WaveSize siz)
    {
        string direction = dir.ToString();
        string size = siz.ToString();

        string path = $"ParticleInfoContainers/Shared/ShockWave/{size}{direction}";
        // Debug.Log(path + " path was created");

        string name = $"{_nameLineStart}{size}{direction}{_nameLineEnd}";
        // Debug.Log(name + " name was created");

        ParticleLowLevel.CellStartRenderParticles(cell, path, name);
    }
}