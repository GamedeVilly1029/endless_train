using System.Collections;
using UnityEngine;

public static class SkillConcrete
{
    // private static TurnProcessor _turnProcessor = GlobalStaticDependancies.TurnProcessor;
    // private static LevelMaster _levelMaster = GlobalStaticDependancies.LevelMaster;

    // public static IEnumerator IncreaseDamageOfNextAttack(IConstructElement element)
    // {
    //     ParticlePlayer.StartBattleCry(_turnProcessor.CurrentActor);
    //     IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
    //     dmgIncreaseEffect.ChildInitializeStatusEffect(_turnProcessor.CurrentActor);

    //     _turnProcessor.CurrentActor.StatusEffectsDuringTurn.Add(dmgIncreaseEffect);
    //     yield return GlobalLowLevelConcrete.Pause;

    //     ParticlePlayer.StopBattleCry(_turnProcessor.CurrentActor);
    // }

    // public static IEnumerator RemoveTantrumVulnerability(IConstructElement element)
    // {
    //     for (int i = _turnProcessor.CurrentActor.StatusEffectsBeforeTakingDamage.Count - 1; i >= 0; i--)
    //     {
    //         if (_turnProcessor.CurrentActor.StatusEffectsBeforeTakingDamage[i] is TantrumVulnerabilityEffect)
    //         {
    //             _turnProcessor.CurrentActor.StatusEffectsBeforeTakingDamage.RemoveAt(i);
    //         }
    //     }
    //     yield return null;
    // }

    // public static IEnumerator AddTantrumVulnerability(IConstructElement element)
    // {
    //     TantrumVulnerabilityEffect tantrum = new TantrumVulnerabilityEffect();
    //     tantrum.ChildInitializeStatusEffect(_turnProcessor.CurrentActor);
    //     _turnProcessor.CurrentActor.StatusEffectsBeforeTakingDamage.Add(tantrum);
    //     yield return null;
    // }

    // public static IEnumerator BeStunned(IConstructElement element)
    // {
    //     yield return GlobalLowLevelConcrete.Pause;
    //     Debug.Log("Stunned!");
    //     yield return GlobalLowLevelConcrete.Pause;
    // }

    // public static IEnumerator StunFirstPlayerActionNextTurnConcrete(IConstructElement element)
    // {
    //     IStatusEffect stunFirstAction = new StunFirstActionEffect();
    //     stunFirstAction.ChildInitializeStatusEffect(_levelMaster.Player);
    //     _levelMaster.Player.StatusEffectsBeforeTurn.Add(stunFirstAction);

    //     yield return GlobalLowLevelConcrete.Pause;
    // }
}