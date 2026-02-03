using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    public Transform Player;
    public List<Cell> Cells;
    public ActionRowController ActionRowController;
    public ActionBeltController ActionBeltController;
    public Action CurrentAction;
    public bool SomeConcreteIsActive;
    public Monster MonsterRefference;
    public GameObject PrefabOfAttack;

    private void Start()
    {
        CreateCellPositionsDictionary();
        Player.position = Cells[0].CellPosition;
        Cells[0].IsOcupiedByEntity = true;
    }

    public IEnumerator IterateThroughActionRow()
    {
        if (ActionRowController.ActionsInRow.Count > 0)
        {
            ActionRowController.ActionsInRow.Reverse();
            for (int i = ActionRowController.ActionsInRow.Count - 1; i >= 0; i--)
            {
                CurrentAction = ActionRowController.ActionsInRow[i];
                CurrentAction.ActionConstruct.Reverse();
                for (int j = CurrentAction.ActionConstruct.Count - 1; j >= 0; j--)
                {
                    yield return CurrentAction.ActionConstruct[j].ExecuteConcrete(this);
                    CurrentAction.ActionConstruct.RemoveAt(j);
                }
                Destroy(ActionRowController.ActionsInRow[i].UIRepresentation);
                ActionRowController.ActionsInRow[i].UIRepresentation = null;

                ActionRowController.ActionsInRow[i] = null;
                ActionRowController.ActionsInRow.RemoveAt(i);
            }
            CurrentAction = null;
        }
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