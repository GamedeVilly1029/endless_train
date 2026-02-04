using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;
using System;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    public List<Cell> Cells;

    public List<Action> CurrentActionRow;
    public Action CurrentAction;

    public bool SomeConcreteIsActive;
    public MonsterActor MonsterRefference;
    public GameObject PrefabOfAttack;

    public PlayerActor PlayerActor;

    private void Start()
    {
        CreateCellPositionsDictionary();
    }

    public IEnumerator IterateThroughActionRow()
    {
        if (CurrentActionRow.Count > 0)
        {
            CurrentActionRow.Reverse();
            for (int i = CurrentActionRow.Count - 1; i >= 0; i--)
            {
                CurrentAction = CurrentActionRow[i];
                CurrentAction.ActionConstruct.Reverse();
                for (int j = CurrentAction.ActionConstruct.Count - 1; j >= 0; j--)
                {
                    yield return CurrentAction.ActionConstruct[j].ExecuteConcrete(this);
                    CurrentAction.ActionConstruct.RemoveAt(j);
                }
                Destroy(CurrentActionRow[i].UIRepresentation);
                CurrentActionRow[i].UIRepresentation = null;

                CurrentActionRow[i] = null;
                CurrentActionRow.RemoveAt(i);
            }
            CurrentAction = null;
        }
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
            cell.IsOcupiedByEntity = false;
            Cells.Add(cell);
        }
    }
}