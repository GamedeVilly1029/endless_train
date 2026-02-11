using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    public Transform Transform{get{return transform;}set{}}
    public List<Action> ActionRow{get;set;} = new();
    public Transform ActionRowPanel;
    public List<Action> Belt = new();
    public Transform BeltPanel;
    public int HP{get;set;} = 10; //Example value
    public int PositionCellIndex { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool IsFacingRight { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        Transform.position = _dungeonMaster.Cells[0].CellPosition;
        _dungeonMaster.Cells[0].EnityOccupyingThisCell = this;
    }

    private void Update()
    {
        TryToDie(HP);
    }

    public void TryToDie(int HP)
    {
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}