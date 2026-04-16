using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public static class LowLevelConcrete
{
    public static WaitForSeconds Pause = new WaitForSeconds(0.5f);

    public static IEnumerator MoveOneCellForward(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!CellBasedCondition.CellAheadExists(dungeonMaster, actor))
        {
            yield break;
        }
        else if (!CellBasedCondition.CellAheadIsEmpty(dungeonMaster, actor))
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return ActorPosManipulation.StepForwardOrBackwards(dungeonMaster, actor, Resources.Load<MoveData>("StepData"), true);
        }
        yield return Pause;
    }

    public static IEnumerator MoveOneCellBackwards(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!CellBasedCondition.CellBehindExists(dungeonMaster, actor))
        {
            yield break;
        }
        else if (!CellBasedCondition.CellBehindIsEmpty(dungeonMaster, actor))
        {
            Debug.Log("Cell ahead isn't empty");
            yield break;
        }
        else
        {
            yield return ActorPosManipulation.StepForwardOrBackwards(dungeonMaster, actor, Resources.Load<MoveData>("StepData"), false);
        }
        yield return Pause;
    }

    public static IActor TryReturnActorAhead(DungeonMaster dungeonMaster, IConstructElement element)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (CellBasedCondition.CellAheadExists(dungeonMaster, actor))
        {
            if (actor.IsFacingRight())
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
  
    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ValueConstructElement casted = element as ValueConstructElement;
        IActor actorAhead = TryReturnActorAhead(dungeonMaster, casted);
        if (actorAhead != null)
        {
            yield return actorAhead.SubtractDamageFromHP(casted.ConcreteValue);
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