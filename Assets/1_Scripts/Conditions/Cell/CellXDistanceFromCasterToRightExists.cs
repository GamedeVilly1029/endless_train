using UnityEngine;

public class CellXDistanceFromCasterToRightExists : BaseConditionCommand
{
    private int _distance;
    private int _baseActorPosIdx;

    public CellXDistanceFromCasterToRightExists
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster,
        int distance,
        int baseActorPosIdx
    ):base(turnProcessor, levelMaster)
    {
        _distance = distance;
        _baseActorPosIdx = baseActorPosIdx;
    }

    public override bool Execute()
    {
        return _baseActorPosIdx + _distance < LevelMasterInst.Cells.Count;
    }
}