using UnityEngine;

public class ActionRowHasOnly1Action : BaseConditionCommand
{
    private BaseActor _caster;
    public ActionRowHasOnly1Action
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
        return _caster.ActionRowInst.Actions.Count == 1;
    }
}