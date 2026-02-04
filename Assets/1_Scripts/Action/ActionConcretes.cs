using UnityEngine;
using System.Collections;
using System.Linq;

public static class ActionConcretes
{
    public static IEnumerator MoveOneCellForward(DungeonMaster master)
    {
        Cell playerCell = master.Cells.FirstOrDefault(x => x.CellPosition == (Vector2)master.PlayerActor.Transform.position);
        int playerCellIndex = master.Cells.IndexOf(playerCell);
        
        if (!ActionConditions.CellAheadExists(master, playerCellIndex))
        {
            yield break;
        }
        else if (!ActionConditions.CellAheadIsEmpty(master, playerCellIndex))
        {
            yield break;
        }
        else
        {
            master.Cells[playerCellIndex].IsOcupiedByEntity = false;
            master.Cells[playerCellIndex + 1].IsOcupiedByEntity = true;
            master.PlayerActor.Transform.position = master.Cells[playerCellIndex + 1].CellPosition;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public static IEnumerator WalkXTiles(DungeonMaster master, int numberOfSteps)
    {
        while (numberOfSteps > 0)
        {
            yield return master.StartCoroutine(MoveOneCellForward(master));
            numberOfSteps--;
        }
        master.SomeConcreteIsActive = false;
    }

    public static IEnumerator AttackEntityAhead(DungeonMaster master, int numberOfAttack)
    {
        Cell playerCell = master.Cells.FirstOrDefault(x => x.CellPosition == (Vector2)master.PlayerActor.Transform.position);
        if (!ActionConditions.CellAheadIsEmpty(master, master.Cells.IndexOf(playerCell)))
        {
            master.MonsterRefference.HP -= numberOfAttack;
            GameObject attackViewObject = Object.Instantiate(
                master.PrefabOfAttack,
                new Vector3(master.PlayerActor.Transform.position.x + 1, master.PlayerActor.Transform.position.y), 
                Quaternion.identity, 
                master.PlayerActor.Transform);
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(attackViewObject);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Entity ahead not exists");
        }
    }
}