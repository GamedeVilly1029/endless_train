using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOtherConcrete : ValueConcrete
{
    private BaseActor _toHeal;
    public HealOtherConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag, 
        int value,
        BaseActor toHeal
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _toHeal = toHeal;
    }

    public override IEnumerator ChildExecute()
    {
        Debug.Log(_toHeal);
        _toHeal.CurrentHP += Value;
        new HealAudioCommand(UnityEngine.Object.FindAnyObjectByType<AudioMaster>()).Execute();
        new PlayHealParticlesGraphicConcrete(_toHeal).Execute();
        new GraphicTransformColorLerpConcrete(_toHeal, Color.green, 0.5f).Execute();
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new HealOtherConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _toHeal);
    }
}