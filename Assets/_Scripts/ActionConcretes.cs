using UnityEngine;
using System.Collections;


public static class ActionConcretes
{
    public static IEnumerator MoveOneCellForwardCoroutine(DungeonMaster master)
    {
        int destinationPositionIndex = master.CellAnchorPositions.IndexOf(master.Player.position) + 1;

        while ((Vector2)master.Player.position != master.CellAnchorPositions[destinationPositionIndex])
        {
            master.Player.position = master.CellAnchorPositions[destinationPositionIndex];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public static IEnumerator WalkTowardsDestinationCell(DungeonMaster master, int destinationCell)
    {
        while ((Vector2)master.Player.position != master.CellAnchorPositions[destinationCell])
        {
            yield return master.StartCoroutine(MoveOneCellForwardCoroutine(master));
        }
    }
}