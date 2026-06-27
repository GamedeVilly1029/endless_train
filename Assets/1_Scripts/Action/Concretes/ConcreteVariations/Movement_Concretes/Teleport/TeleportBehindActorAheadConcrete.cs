using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehindActorAheadConcrete : BaseConcrete
{
    private BaseActor _caster;
    public TeleportBehindActorAheadConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag,
        BaseActor caster
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
        _caster = caster;
    }

    public override IEnumerator ChildExecute()
    {
        BaseActor teleportBehind = GlobalLowLevelConcrete.TryReturnFirstActorOnCellsAhead(TurnProcessorInst, LevelMasterInst, _caster);
        yield return new TeleportBehindConcrete(TurnProcessorInst, LevelMasterInst, ActionOfThisConcrete, null, ActionConcreteTag.Skill, _caster, teleportBehind).Execute();
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new TeleportBehindActorAheadConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, _caster);
    }
}