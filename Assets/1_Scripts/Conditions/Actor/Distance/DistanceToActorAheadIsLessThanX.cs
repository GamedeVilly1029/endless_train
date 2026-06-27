using System;
using UnityEngine;

public class DistanceToActorAheadIsLessThanX : BaseConditionCommand
{
    private int _value;
    private BaseActor _caster;
    public DistanceToActorAheadIsLessThanX(TurnProcessor turnProcessor, LevelMaster levelMaster, int value, BaseActor caster) : base(turnProcessor, levelMaster)
    {
        _value = value;
        _caster = caster;
    }

    public override bool Execute()
    {
        BaseActor actorAhead = GlobalLowLevelConcrete.TryReturnFirstActorOnCellsAhead(TurnProcessorInst, LevelMasterInst, _caster);
        if (actorAhead == null)
        {
            return false;
        }
        return Math.Max(0, Mathf.Abs(_caster.PositionCellIndex - actorAhead.PositionCellIndex) - 1) < _value;
    }
}