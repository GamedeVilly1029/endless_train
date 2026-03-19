using System.Collections;
using UnityEngine;

public static class LowLevelConcrete
{
    public static WaitForSeconds Pause = new WaitForSeconds(0.5f);

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
            yield return ActorPosManipulation.ChangeIndexAndPosition(dungeonMaster, actor, Resources.Load<MoveData>("StepData"));
        }
        yield return Pause;
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
            // Debug.Log("Cell ahead isn't exists");
            return null;
        }
    }

    public static IEnumerator HitActorAhead(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        if (!ActionConditions.CellAheadIsEmpty(dungeonMaster, dungeonMaster.CurrentActor))
        {
            yield return AttackEntityAhead(dungeonMaster, element);
        }
        else
        {
            GameObject attackViewObject = DrawStrikeUI(dungeonMaster);
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            yield return Pause;
            Object.Destroy(attackViewObject);
        }
    }
    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
        if (actorAhead != null)
        {
            yield return actorAhead.SubtractDamageFromHP(element.ConcreteValue);
            yield return actorAhead.RunBeforeDamageStatuses();

            GameObject attackViewObject = DrawStrikeUI(dungeonMaster);
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            yield return Pause;
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
            Object.Destroy(attackViewObject);
        }
    }

    public static GameObject DrawStrikeUI(DungeonMaster dungeonMaster)
    {
        GameObject attackViewObject;
        if (dungeonMaster.CurrentActor.IsFacingRight)
        {
            attackViewObject = Object.Instantiate(
                Resources.Load<GameObject>("AttackVisual"),
                new Vector3(dungeonMaster.CurrentActor.Transform.position.x + 1, dungeonMaster.CurrentActor.Transform.position.y),
                Quaternion.identity,
                dungeonMaster.CurrentActor.Transform);
        }
        else
        {
            attackViewObject = Object.Instantiate(
                Resources.Load<GameObject>("AttackVisual"),
                new Vector3(dungeonMaster.CurrentActor.Transform.position.x - 1, dungeonMaster.CurrentActor.Transform.position.y),
                Quaternion.identity,
                dungeonMaster.CurrentActor.Transform);
        }
        return attackViewObject;
    }
}
