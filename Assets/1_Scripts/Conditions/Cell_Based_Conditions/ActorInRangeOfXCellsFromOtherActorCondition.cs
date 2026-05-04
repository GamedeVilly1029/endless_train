using UnityEngine;

public class ActorInRangeOfXCellsFromOtherActorCondition : BaseConditionCommand
{
    private IActor _actorToFind;
    private IActor _caster;
    private int _range;

    public ActorInRangeOfXCellsFromOtherActorCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, IActor actorToFind, IActor caster, int range)
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
            if (rangeTemp + cellIndex < LevelMaster.Cells.Count)
            {
                if (LevelMaster.Cells[rangeTemp + cellIndex].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
            if (cellIndex - rangeTemp >= 0)
            {
                if (LevelMaster.Cells[cellIndex - rangeTemp].EnityOccupyingThisCell == _actorToFind)
                {
                    return true;
                }
            }
            rangeTemp--;
        }
        return false;
    }
}