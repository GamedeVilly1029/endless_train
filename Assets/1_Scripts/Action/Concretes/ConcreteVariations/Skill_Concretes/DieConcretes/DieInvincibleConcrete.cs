using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInvincibleConcrete : BaseConcrete
{
    BaseActor _suicider;
    public DieInvincibleConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag,
        BaseActor suicider
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _suicider = suicider;
    }

    public override IEnumerator ChildExecute()
    {
        yield return GlobalLowLevelConcrete.Pause;
        Object.FindAnyObjectByType<AudioMaster>().PlaySound("explosion");
        _suicider.MakeInvincible();
        _suicider.Die();
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new DieConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _suicider);
    }
}