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
        IAction actionOfThisConcrete, 
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
            Debug.Log("Healed by 15 percent");
            yield return null;
        }
        else
        {
            _caster.CurrentHP = _caster.MaxHP;
            Debug.Log("Healed to full");
            yield return null;
        }
        new HealAudioCommand(UnityEngine.Object.FindFirstObjectByType<AudioMaster>()).Execute();
        new PlayHealParticlesGraphicConcrete(_caster).Execute();
        new GraphicTransformColorLerpConcrete(_caster, Color.green, 0.5f).Execute();
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new HealPercentConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _caster);
    }
}
