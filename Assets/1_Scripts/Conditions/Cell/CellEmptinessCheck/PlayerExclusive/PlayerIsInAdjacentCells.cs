using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerIsInAdjacentCells : BaseConditionCommand
{
    private BaseActor _caster;
    public PlayerIsInAdjacentCells
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        BaseActor caster
    ):base(turnProcessor, levelMaster)
    {
        _caster = caster;
    }

    public override bool Execute()
    {
        if (!new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex - 1).Execute())
        {
            if (LevelMasterInst.Cells[_caster.PositionCellIndex - 1].EnityOccupyingThisCell is PlayerActor)
            {
                return true;
            }
            return false;
        }
        else if (!new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex + 1).Execute())
        {
            if (LevelMasterInst.Cells[_caster.PositionCellIndex + 1].EnityOccupyingThisCell is PlayerActor)
            {
                return true;
            }
            return false;
        }
        return false; 
    }
}