using UnityEngine;

public class BaseConditionCommand : IConditionCommand
{
    public TurnProcessor TurnProcessorInst;
    public LevelMaster LevelMasterInst;


    public BaseConditionCommand(TurnProcessor turnProcessor, LevelMaster levelMaster)
    {
        TurnProcessorInst = turnProcessor;
        LevelMasterInst = levelMaster;
    }

    public virtual bool Execute()
    {
        Debug.LogWarning("Base version of the condition's Execute was called. Implement concrete condition properly");
        return false;
    }
}