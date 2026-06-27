using UnityEngine;

public class TargetIsRightFromCaster : BaseConditionCommand
{
    private BaseActor _caster;
    private BaseActor _target;
    public TargetIsRightFromCaster(TurnProcessor turnProcessor, LevelMaster levelMaster, BaseActor caster, BaseActor target) : base(turnProcessor, levelMaster)
    {
        _caster = caster;
        _target = target;
    }

    public override bool Execute()
    {
        return _caster.PositionCellIndex - _target.PositionCellIndex < 0;
    }
}
