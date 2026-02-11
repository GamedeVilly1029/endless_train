using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    [SerializeField] private TurnMaster _turnMaster;
    public List<Cell> Cells;

    public List<Action> CurrentActionRow;
    public Action CurrentAction;
    public IActor CurrentActor;

    public MonsterActor MonsterRefference;
    public PlayerActor PlayerActor;

    public List<IActor> AllMonsters;

    private void Start()
    {
        AllMonsters = new();
        CreateCellPositionsDictionary();
    }

    public IEnumerator IterateThroughActionRow()
    {
        CurrentActionRow = CreateMutualActionRow();
        foreach (Action action in CurrentActionRow)
        {
            CurrentAction = action;
            CurrentActor = action.Actor;
            foreach (ActionConstructElement element in action.ActionConstruct)
            {
                yield return element.ExecuteConcrete(this);
            }
            if (action.UIRepresentation != null)
            {
                Destroy(action.UIRepresentation);
                action.UIRepresentation = null;
            }
        }
        CurrentAction = null;
        CurrentActor = null;
        CurrentActionRow.Clear();
        
    }

    private List<Action> CreateMutualActionRow()
    {
        List<Action> row = new();
        int maxPossibleElement = Mathf.Max(PlayerActor.ActionRow.Count, MonsterRefference.ActionRow.Count) - 1;
        for (int i = 0; i <= maxPossibleElement; i++)
        {
            if (i <= PlayerActor.ActionRow.Count - 1)
            {
                row.Add(PlayerActor.ActionRow[i]);
            }
            if (i <= MonsterRefference.ActionRow.Count - 1)
            {
                row.Add(MonsterRefference.ActionRow[i]);
            }
        }
        return row;
    }

    private void CreateCellPositionsDictionary()
    {
        Cells = new();
        foreach (Transform transform in MovementCellAnchors)
        {
            Cell cell = new();
            cell.CellPosition = transform.position;
            cell.EnityOccupyingThisCell = null;
            Cells.Add(cell);
        }
    }
}