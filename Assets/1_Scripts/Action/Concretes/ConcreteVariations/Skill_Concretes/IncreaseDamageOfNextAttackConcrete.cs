using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageOfNextAttackConcrete : BaseConcrete
{
    public IncreaseDamageOfNextAttackConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        ParticlePlayer.StartBattleCry(TurnProcessorInst.CurrentActor);
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.ChildInitializeStatusEffect(TurnProcessorInst.CurrentActor);

        TurnProcessorInst.CurrentActor.StatusEffectsDuringTurn.Add(dmgIncreaseEffect);
        yield return GlobalLowLevelConcrete.Pause;

        ParticlePlayer.StopBattleCry(TurnProcessorInst.CurrentActor);
    }
}