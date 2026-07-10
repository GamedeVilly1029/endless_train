using UnityEngine;

public class CellAtIdxIsEmpty : BaseConditionCommand
{
    private int _idx;
    public CellAtIdxIsEmpty(TurnProcessor turnProcessor, LevelMaster levelMaster, int idx) : base(turnProcessor, levelMaster)
    {
        _idx = idx;
    }

    public override bool Execute()
    {
        if (new CellAtIdxExists(TurnProcessorInst, LevelMasterInst, _idx).Execute())
        {
            // Debug.Log($"cell {_idx} exists");
            return LevelMasterInst.Cells[_idx].EnityOccupyingThisCell == null;
        }
        // Debug.Log($"cell {_idx} doesn't exists");
        return false;
    }
}