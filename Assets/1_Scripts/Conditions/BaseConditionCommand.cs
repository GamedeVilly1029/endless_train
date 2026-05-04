using UnityEngine;

public class BaseConditionCommand : IConditionCommand
{
    public TurnProcessor TurnProcessor;
    public LevelMaster LevelMaster;


    public BaseConditionCommand(TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        TurnProcessor = turnProcessor;
        LevelMaster = levelMaster;
    }

    public virtual bool Execute()
    {
        Debug.LogWarning("Base version of the condition's Execute was called. Implement concrete condition properly");
        return false;
    }
}