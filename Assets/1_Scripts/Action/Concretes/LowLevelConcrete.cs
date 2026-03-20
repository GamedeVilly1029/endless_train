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
  
    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster, ActionConstructElement element)
    {
        IActor actorAhead = TryReturnActorAhead(dungeonMaster, element);
        if (actorAhead != null)
        {
            yield return actorAhead.SubtractDamageFromHP(element.ConcreteValue);
            yield return actorAhead.RunBeforeDamageStatuses();

            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(dungeonMaster.CurrentActor);
            yield return Pause;
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("hit");
            ParticlePlayer.StopStrike(dungeonMaster.CurrentActor);
        }
        else
        {
            Object.FindFirstObjectByType<AudioMaster>().PlaySound("swing");
            ParticlePlayer.StartStrike(dungeonMaster.CurrentActor);
            yield return Pause;
            ParticlePlayer.StopStrike(dungeonMaster.CurrentActor);
        }
    }
}