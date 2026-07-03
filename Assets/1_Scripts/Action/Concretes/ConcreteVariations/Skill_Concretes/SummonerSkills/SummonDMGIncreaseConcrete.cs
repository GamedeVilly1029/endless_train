using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDMGIncreaseConcrete : BaseConcrete
{
    private Summoner _summoner;
    private Summon _summon;
    public SummonDMGIncreaseConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag, 
        Summoner summoner,
        Summon summon
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _summoner = summoner;
        _summon = summon;
    }

    public override IEnumerator ChildExecute()
    {
        IStatusEffect dmgUp = new NextAttackDmgUpEffect();
        dmgUp.Initialize(TurnProcessorInst, LevelMasterInst, _summon);
        _summon.StatusEffectsDuringTurn.Add(dmgUp);
        _summon.StartCoroutine(new GraphicTransformColorLerpConcrete(_summon, Color.red, 0.5f).Execute());

        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SummonDMGIncreaseConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _summoner, _summon);
    }
}