using UnityEngine;
using System.Collections;

public static class ActionConcrete
{
    public static IEnumerator WalkXTilesForward(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        int stepsToSubtract = casted.ConcreteValue;
        while (stepsToSubtract > 0)
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return dungeonMaster.StartCoroutine(LowLevelConcrete.MoveOneCellForward(dungeonMaster, element));

            stepsToSubtract--;
        }
    }

    public static IEnumerator WalkXTilesBackwards(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        int stepsToSubtract = casted.ConcreteValue;
        while (stepsToSubtract > 0)
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return dungeonMaster.StartCoroutine(LowLevelConcrete.MoveOneCellBackwards(dungeonMaster, element));

            stepsToSubtract--;
        }
    }

    public static IEnumerator StrikeConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return LowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
    }


    public static IEnumerator RotateConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return ActorPosManipulation.RotateActor(dungeonMaster.CurrentActor);
        Debug.Log($"{dungeonMaster.CurrentActor} was rotated");
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator Push(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ParticlePlayer.StartPush(dungeonMaster.CurrentActor);
        if (CellBasedCondition.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
        {
            IActor actorAhead = LowLevelConcrete.TryReturnActorAhead(dungeonMaster, element);
            if (actorAhead != null)
            {
                if (CellBasedCondition.AdjacentCellsExists(dungeonMaster, actorAhead))
                {
                    yield return ActorPosManipulation.BePushed(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight());
                }
            }
        }
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return LowLevelConcrete.Pause;
        ParticlePlayer.StopPush(dungeonMaster.CurrentActor);
    }

    public static IEnumerator IncreaseDamageOfNextAttack(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ParticlePlayer.StartBattleCry(dungeonMaster.CurrentActor);
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.InitializeStatusEffect(dungeonMaster);

        dungeonMaster.CurrentActor.StatusEffectsForTurn.Add(dmgIncreaseEffect);
        // Debug.Log("Damage of following attack is increased");
        yield return LowLevelConcrete.Pause;

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

    public static IEnumerator TantrumStrike(DungeonMaster dungeonMaster, IConstructElement element)
    {
        for (int i = dungeonMaster.CurrentActor.FightBasedActionHistory.Count - 1; i >= 0; i--)
        {
            if (dungeonMaster.CurrentActor.FightBasedActionHistory[i] is Tantrum)
            {
                yield return LowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
            }
            else
            {
                break;
            }
        }
        yield return LowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
    }

    public static IEnumerator BeStunned(DungeonMaster dungeonMaster, IConstructElement element)
    {
        Debug.Log("Stunned");
        yield return LowLevelConcrete.Pause;
        yield return new WaitForSeconds(1.5f);
    }
}