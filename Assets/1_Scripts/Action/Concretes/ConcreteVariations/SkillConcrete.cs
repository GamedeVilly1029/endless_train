using System.Collections;
using UnityEngine;

public static class SkillConcrete
{
    public static IEnumerator IncreaseDamageOfNextAttack(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ParticlePlayer.StartBattleCry(dungeonMaster.CurrentActor);
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.InitializeStatusEffect(dungeonMaster);

        dungeonMaster.CurrentActor.StatusEffectsDuringTurn.Add(dmgIncreaseEffect);
        yield return GlobalLowLevelConcrete.Pause;

        ParticlePlayer.StopBattleCry(dungeonMaster.CurrentActor);
    }

    public static IEnumerator RemoveTantrumVulnerability(DungeonMaster dungeonMaster, IConstructElement element)
    {
        for (int i = dungeonMaster.CurrentActor.StatusEffectsBeforeTakingDamage.Count - 1; i >= 0; i--)
        {
            if (dungeonMaster.CurrentActor.StatusEffectsBeforeTakingDamage[i] is TantrumVulnerabilityEffect)
            {
                dungeonMaster.CurrentActor.StatusEffectsBeforeTakingDamage.RemoveAt(i);
            }
        }
        yield return null;
    }

    public static IEnumerator AddTantrumVulnerability(DungeonMaster dungeonMaster, IConstructElement element)
    {
        TantrumVulnerabilityEffect tantrum = new TantrumVulnerabilityEffect();
        tantrum.InitializeStatusEffect(dungeonMaster);
        dungeonMaster.CurrentActor.StatusEffectsBeforeTakingDamage.Add(tantrum);
        yield return null;
    }

    public static IEnumerator BeStunned(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return GlobalLowLevelConcrete.Pause;
        Debug.Log("Stunned!");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public static IEnumerator StunFirstPlayerActionNextTurnConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IStatusEffect stunFirstAction = new StunFirstActionEffect();
        stunFirstAction.InitializeStatusEffect(dungeonMaster);
        dungeonMaster.Player.StatusEffectsBeforeTurn.Add(stunFirstAction);

        Debug.Log("StunFirstPlayerActionNextTurnConcrete is executed");

        yield return GlobalLowLevelConcrete.Pause;
    }
}