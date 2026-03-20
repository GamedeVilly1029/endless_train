using UnityEngine;
using System.Collections;

public static class ActionConcrete
{
    public static IEnumerator WalkXTiles(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        int stepsToSubtract = element.ConcreteValue;
        while (stepsToSubtract > 0)
        {

            Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return dungeonMaster.StartCoroutine(LowLevelConcrete.MoveOneCellForward(dungeonMaster, element));

            stepsToSubtract--;
        }
    }

    public static IEnumerator StrikeConcrete(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        yield return LowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
    }


    public static IEnumerator RotateActor(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        dungeonMaster.CurrentActor.IsFacingRight = !dungeonMaster.CurrentActor.IsFacingRight;
        // Debug.Log($"{dungeonMaster.CurrentActor} was rotated");
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator Push(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        if (ActionConditions.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
        {
            IActor actorAhead = LowLevelConcrete.TryReturnActorAhead(dungeonMaster, element);
            if (actorAhead != null)
            {
                if (ActionConditions.AdjacentCellsExists(dungeonMaster, actorAhead))
                {
                    yield return ActorPosManipulation.PushActor(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight);
                }
            }
        }
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator IncreaseDamageOfNextAttack(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.InitializeStatusEffect(dungeonMaster);

        dungeonMaster.CurrentActor.StatusEffectsForTurn.Add(dmgIncreaseEffect);
        // Debug.Log("Damage of following attack is increased");
        yield return LowLevelConcrete.Pause;
    }

    public static IEnumerator RemoveTantrumVulnerability(DungeonMaster dungeonMaster, ActionConstructElement element)
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

    public static IEnumerator AddTantrumVulnerability(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        TantrumVulnerabilityEffect tantrum = new TantrumVulnerabilityEffect();
        tantrum.InitializeStatusEffect(dungeonMaster);
        dungeonMaster.CurrentActor.StatusEffectsBeforeTakingDamage.Add(tantrum);
        yield return null;
    }

    public static IEnumerator TantrumStrike(DungeonMaster dungeonMaster, ActionConstructElement element)
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
}