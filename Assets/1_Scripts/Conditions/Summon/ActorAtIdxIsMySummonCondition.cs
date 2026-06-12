using UnityEngine;

public class ActorAtIdxIsMySummonCondition : BaseConditionCommand
{
    private int _idx;
    private Summoner _summoner;
    public ActorAtIdxIsMySummonCondition
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        int idx,
        Summoner summoner
    ):base(turnProcessor, levelMaster)
    {
        _idx = idx;
        _summoner = summoner;
    }

    public override bool Execute()
    {
        if (!new CellAtIdxExists(TurnProcessorInst,LevelMasterInst, _idx).Execute())
        {
            return false;
        }
        if (new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _idx).Execute())
        {
            return false;
        }

        BaseActor summon = LevelMasterInst.Cells[_idx].EnityOccupyingThisCell;
        if (summon is not Summon)
        {
            return false;
        }
        

        if (_summoner.Summons.Contains((Summon)summon))
        {
            return true;
        }

        return false;
    }
}