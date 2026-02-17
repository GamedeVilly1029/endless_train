using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterActor : MonoBehaviour, IActor, IMonster
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private TextMeshPro HPBarText;
    [SerializeField] private RectTransform _actionRowPanel;

    public int HP{get;set;}
    public Transform Transform{get {return transform;} set{}}
    public List<IAction> ActionRow { get; set;} = new();
    public RectTransform ActionRowPanel{get{return _actionRowPanel;}set{}}
    public int PositionCellIndex {get;set;}
    public Stack<int> PositionCellIndexHistory{get;set;}
    public bool IsFacingRight {get;set;}
    public MonsterTypes MonsterType {get;set;}

    public void Initialize()
    {
        _dungeonMaster.MonstersWithActorReference.Add(this, this);
        _dungeonMaster.AllActors.Add(this);
        Transform.position = _dungeonMaster.Cells[9].CellPosition;
        _dungeonMaster.Cells[9].EnityOccupyingThisCell = this;
        PositionCellIndex = 9;
        IsFacingRight = false;
        MonsterType = MonsterTypes.glist1;

        HP = 10;
    }

    private void Update()
    {
        HPBarText.text = HP.ToString();
        TryToDie(HP);
    }

    public void TryToDie(int HP)
    {
        if (HP <= 0)
        {
            Debug.Log("Death");
            Destroy(gameObject);
        }
    }
}