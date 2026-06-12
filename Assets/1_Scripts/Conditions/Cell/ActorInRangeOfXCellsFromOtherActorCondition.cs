using UnityEngine;

public class ActorInRangeOfXCellsFromOtherActorCondition : BaseConditionCommand
{
    private BaseActor _actorToFind;
    private BaseActor _caster;
    private int _range;

    public ActorInRangeOfXCellsFromOtherActorCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor actorToFind, BaseActor caster, int range)
        : base(turnProcessor, levelMaster)
    {
        _actorToFind = actorToFind;
        _caster = caster;
        _range = range;
    }

    public override bool Execute()
    {
        int cellIndex = _caster.PositionCellIndex;

        int rangeTemp = _range;
        while (rangeTemp > 0)
        {
            if (rangeTemp + cellIndex < LevelMasterInst.Cells.Count)
            {
                if (LevelMasterInst.Cells[rangeTemp + cellIndex].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
            if (cellIndex - rangeTemp >= 0)
            {
                if (LevelMasterInst.Cells[cellIndex - rangeTemp].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
            rangeTemp--;
        }
        return false;
    }
}