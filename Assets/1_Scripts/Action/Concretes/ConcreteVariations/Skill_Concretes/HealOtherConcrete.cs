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
        _toHeal.CurrentHP += Value;
        new HealAudioCommand(UnityEngine.Object.FindAnyObjectByType<AudioMaster>()).Execute();
        new PlayHealParticlesGraphicConcrete(_toHeal).Execute();
        _toHeal.StartCoroutine(new GraphicTransformColorLerpConcrete(_toHeal, Color.green, 0.25f).Execute()) ;
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override List<IConditionCommand> CreateBaseConditionList()
    {
        return new List<IConditionCommand>()
        {
            new ActorIsNotNullCondition(TurnProcessorInst, LevelMasterInst, _toHeal)
        };
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new HealOtherConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _toHeal);
    }
}