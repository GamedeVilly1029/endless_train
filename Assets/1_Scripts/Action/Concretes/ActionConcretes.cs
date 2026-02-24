using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public static class ActionConcretes
{
    public static IEnumerator MoveOneCellForward(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!ActionConditions.CellAheadExists(dungeonMaster, actor))
        {
            yield break;
        }
        else if (!ActionConditions.CellAheadIsEmpty(dungeonMaster, actor))
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            ActorInWorldManipulationUtils.ChangeIndexAndPosition(dungeonMaster, actor);
        }
        yield return new WaitForSeconds(0.5f);
    }

    public static IEnumerator WalkXTiles(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        int stepsToSubtract = element.ConcreteValue;
        while (stepsToSubtract > 0)
        {
            yield return dungeonMaster.StartCoroutine(MoveOneCellForward(dungeonMaster, element));
            stepsToSubtract--;
        }
    }

    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        if (!ActionConditions.CellAheadIsEmpty(dungeonMaster, dungeonMaster.CurrentActor))
        {
            if (dungeonMaster.CurrentActor.IsFacingRight)
            {
                IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
                if (actorAhead != null)
                {
                    yield return actorAhead.TakeDamage(element.ConcreteValue); 
                    GameObject attackViewObject = Object.Instantiate(
                        Resources.Load<GameObject>("AttackVisual"),
                        new Vector3(dungeonMaster.CurrentActor.Transform.position.x + 1, dungeonMaster.CurrentActor.Transform.position.y),
                        Quaternion.identity,
                        dungeonMaster.CurrentActor.Transform);
                    yield return new WaitForSeconds(0.5f);
                    Object.Destroy(attackViewObject);
                }
            }
            else
            {
                IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
                if (actorAhead != null)
                {
                    yield return actorAhead.TakeDamage(element.ConcreteValue); 
                    GameObject attackViewObject = Object.Instantiate(
                        Resources.Load<GameObject>("AttackVisual"),
                        new Vector3(dungeonMaster.CurrentActor.Transform.position.x - 1, dungeonMaster.CurrentActor.Transform.position.y),
                        Quaternion.identity,
                        dungeonMaster.CurrentActor.Transform);
                    yield return new WaitForSeconds(0.5f);
                    Object.Destroy(attackViewObject);
                }
            }
        }
        else
        {
            GameObject attackViewObject = Object.Instantiate(
                Resources.Load<GameObject>("AttackVisual"),
                new Vector3(dungeonMaster.PlayerActor.Transform.position.x + 1, dungeonMaster.PlayerActor.Transform.position.y), 
                Quaternion.identity, 
                dungeonMaster.PlayerActor.Transform);
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(attackViewObject);
        }
    }

    public static IActor TryReturnActorAhead(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (ActionConditions.CellAheadExists(dungeonMaster, actor))
        {
            if (actor.IsFacingRight)
            {
                if (dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell != null)
                {
                    return dungeonMaster.Cells[actor.PositionCellIndex + 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell != null)
                {
                    return dungeonMaster.Cells[actor.PositionCellIndex - 1].EnityOccupyingThisCell;
                }
                else
                {
                    return null;
                }
            }
        }
        else
        {
            Debug.Log("Cell ahead isn't exists");
            return null;
        }
    }

    public static IEnumerator RotateActor(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        dungeonMaster.CurrentActor.IsFacingRight = !dungeonMaster.CurrentActor.IsFacingRight;
        Debug.Log($"{dungeonMaster.CurrentActor} was rotated");
        yield return new WaitForSeconds(0.5f);
    }

    public static IEnumerator Push(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        if (dungeonMaster.CurrentActor.IsFacingRight)
        {
            if (ActionConditions.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
            {
                IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
                if (actorAhead != null)
                {
                    if (ActionConditions.AdjacentCellsExists(dungeonMaster, actorAhead))
                    {
                        ActorInWorldManipulationUtils.PushActor(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight);
                    }
                }
            }
        }
        else
        {
            if (ActionConditions.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
            {
                IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
                if (actorAhead != null)
                {
                    if (ActionConditions.AdjacentCellsExists(dungeonMaster, actorAhead))
                    {
                        ActorInWorldManipulationUtils.PushActor(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight);
                    }
                }
            } 
        }
        yield return new WaitForSeconds(0.5f);
    }

    public static IEnumerator IncreaseDamageOfNextAttackConcrete(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IStatusEffect dmgIncreaseEffect = new NextAttackDmgUpEffect();
        dmgIncreaseEffect.InitializeStatusEffect(dungeonMaster);

        dungeonMaster.CurrentActor.StatusEffectsForTurn.Add(dmgIncreaseEffect);
        Debug.Log("Damage of following attack is increased");
        yield return new WaitForSeconds(0.5f);
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
                yield return AttackEntityAhead(dungeonMaster, element);
            }
            else
            {
                break;
            }
        }
        yield return AttackEntityAhead(dungeonMaster, element);
    }
}