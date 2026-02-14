using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private RectTransform _actionRowPanel;

    public Transform Transform{get{return transform;}set{}}
    public List<Action> ActionRow{get;set;} = new();
    public RectTransform ActionRowPanel{get{return _actionRowPanel;}set{}}
    public List<Action> Belt = new();
    public Transform BeltPanel;
    public int HP{get;set;} = 10; //Example value
    public int PositionCellIndex {get;set;}
    public bool IsFacingRight {get;set;}

    public void Initialize()
    {
        Transform.position = _dungeonMaster.Cells[0].CellPosition;
        _dungeonMaster.Cells[0].EnityOccupyingThisCell = this;
        PositionCellIndex = 0;
        IsFacingRight = true;
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