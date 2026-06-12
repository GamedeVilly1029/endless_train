using UnityEngine;

public class CellAtIdxExists : BaseConditionCommand
{
    private int _idx;
    public CellAtIdxExists(TurnProcessor turnProcessor, LevelMaster levelMaster, int idx) : base(turnProcessor, levelMaster)
    {
        _idx = idx;
    }

    public override bool Execute()
    {
        return _idx >= 0 && _idx < 10;
    }
}