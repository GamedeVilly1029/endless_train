using UnityEngine;

public class CellAfterFirstActorAheadIsEmpty : BaseConditionCommand
{
    public BaseActor _caster;
    public CellAfterFirstActorAheadIsEmpty
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
        BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnFirstActorOnCellsAhead(TurnProcessorInst, LevelMasterInst, _caster);
        if (actorAhead == null)
        {
            return false;
        }
        int idxToCheck = new TargetIsRightFromCaster(TurnProcessorInst, LevelMasterInst, _caster, actorAhead).Execute() ? actorAhead.PositionCellIndex + 1 : actorAhead.PositionCellIndex - 1;

        return new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, idxToCheck).Execute();
    }
}