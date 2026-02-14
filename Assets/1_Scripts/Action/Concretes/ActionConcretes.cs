using UnityEngine;
using System.Collections;
using System.Linq;

public static class ActionConcretes
{
    public static IEnumerator MoveOneCellForward(DungeonMaster dungeonMaster)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (!ActionConditions.CellAheadExists(dungeonMaster))
        {
            yield break;
        }
        else if (!ActionConditions.CellAheadIsEmpty(dungeonMaster))
        {
            yield break;
        }
        else
        {
            ActorInWorldManipulationUtils.ChangeIndexAndPosition(dungeonMaster, actor);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public static IEnumerator WalkXTiles(DungeonMaster dungeonMaster)
    {
        while (dungeonMaster.CurrentAction.ValueForActionConcrete > 0)
        {
            yield return dungeonMaster.StartCoroutine(MoveOneCellForward(dungeonMaster));
            dungeonMaster.CurrentAction.ValueForActionConcrete--;
        }
    }

    public static IEnumerator AttackEntityAhead(DungeonMaster dungeonMaster)
    {
        if (!ActionConditions.CellAheadIsEmpty(dungeonMaster))
        {
            if (dungeonMaster.CurrentActor.IsFacingRight)
            {
                IActor actorAhead = TryReturnActorAhead(dungeonMaster);
                if (actorAhead != null)
                {
                    actorAhead.HP -= dungeonMaster.CurrentAction.ValueForActionConcrete; 
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
                IActor actorAhead = TryReturnActorAhead(dungeonMaster);
                if (actorAhead != null)
                {
                    actorAhead.HP -= dungeonMaster.CurrentAction.ValueForActionConcrete; 
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

    public static IActor TryReturnActorAhead(DungeonMaster dungeonMaster)
    {
        IActor actor = dungeonMaster.CurrentActor;
        if (ActionConditions.CellAheadExists(dungeonMaster))
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
}