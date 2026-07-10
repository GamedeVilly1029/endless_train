using UnityEngine;

public class PlayerIsInAdjacentCells : BaseConditionCommand
{
    private BaseActor _caster;
    public PlayerIsInAdjacentCells
    (
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        BaseActor caster
    ) : base(turnProcessor, levelMaster)
    {
        _caster = caster;
    }

    public override bool Execute()
    {
        bool existsToRight = false;
        bool existsToLeft = false;

        if (new CellAtIdxExists(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex - 1).Execute())
        {
            if (!new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex - 1).Execute())
            {
                {
                    Debug.Log("Player stands to the left");
                    existsToLeft = true;
                }
            }
        }
        if (new CellAtIdxExists(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex + 1).Execute())
        {
            if (!new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _caster.PositionCellIndex + 1).Execute())
            {
                if (LevelMasterInst.Cells[_caster.PositionCellIndex + 1].EnityOccupyingThisCell is PlayerActor)
                {
                    Debug.Log("Player stands to the right");
                    existsToRight = true;
                }
            }
        }
        Debug.Log($"Hence, {existsToLeft || existsToRight}");
        return existsToLeft || existsToRight;
    }
}