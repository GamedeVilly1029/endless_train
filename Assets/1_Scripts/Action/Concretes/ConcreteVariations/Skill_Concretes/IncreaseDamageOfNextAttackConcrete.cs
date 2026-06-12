using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageOfNextAttackConcrete : BaseConcrete
{
    BaseActor _cryer;
    public IncreaseDamageOfNextAttackConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    BaseAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    BaseActor cryer
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _cryer = cryer;
    }

    public override IEnumerator ChildExecute()
    {
        ActorParticlePlayer.PlayParticles(_cryer, ParticleType.BattleCry);
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.Initialize(TurnProcessorInst, LevelMasterInst, TurnProcessorInst.CurrentActor);

        TurnProcessorInst.CurrentActor.StatusEffectsDuringTurn.Add(dmgIncreaseEffect);
        yield return GlobalLowLevelConcrete.Pause;

    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new IncreaseDamageOfNextAttackConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _cryer);
    }
}