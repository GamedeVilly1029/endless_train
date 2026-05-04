using UnityEngine;

public static class GlobalStaticDependancies
{
    public static TurnProcessor TurnProcessor;
    public static LevelMaster LevelMaster;

    public static void InitDependancies(TurnProcessor turnProcessorSet, LevelMaster levelMasterSet)
    {
        TurnProcessor = turnProcessorSet;
        LevelMaster = levelMasterSet;

        if (TurnProcessor == null || LevelMaster == null)
        {
            Debug.LogError("CellBasedCondition's dependancies are missing!");
        }
    }
}
