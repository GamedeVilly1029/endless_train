using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPercentConcrete : ValueConcrete
{
    private BaseActor _caster;
    public HealPercentConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag, 
        int value,
        BaseActor caster
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _caster = caster;
    }

    public override IEnumerator ChildExecute()
    {
        float HP = _caster.CurrentHP;
        if (HP + HP/100*Value <= _caster.MaxHP)
        {
            _caster.CurrentHP += Mathf.RoundToInt(HP/100*Value);
            yield return null;
        }
        else
        {
            _caster.CurrentHP = _caster.MaxHP;
            yield return null;
        }
        new HealAudioCommand(UnityEngine.Object.FindAnyObjectByType<AudioMaster>()).Execute();
        new PlayHealParticlesGraphicConcrete(_caster).Execute();
        new GraphicTransformColorLerpConcrete(_caster, Color.green, 0.25f).Execute();
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new HealPercentConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _caster);
    }
}
