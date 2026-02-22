using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private RectTransform _actionRowPanel;
    [SerializeField] private TextMeshPro HPBarText;

    public Transform Transform{get{return transform;}set{}}
    public List<IAction> ActionRow{get;set;} = new();
    public RectTransform ActionRowPanel{get{return _actionRowPanel;}set{}}
    public List<IAction> Belt = new();
    public Transform BeltPanel;
    public int HP{get;set;} = 10; //Example value
    public int PositionCellIndex {get;set;}
    public Stack<int> PositionCellIndexHistory{get;set;}
    public bool IsFacingRight {get;set;}
    public List<IStatusEffect> StatusEffectsForTurn {get;set;}
    public List<IAction> FightBasedActionHistory{get;set;}


    public void Initialize()
    {
        _dungeonMaster.AllActors.Add(this);
        Transform.position = _dungeonMaster.Cells[0].CellPosition;
        _dungeonMaster.Cells[0].EnityOccupyingThisCell = this;
        PositionCellIndex = 0;
        IsFacingRight = true;
        StatusEffectsForTurn = new();
    }


    private void Update()
    {
        TryToDie(HP);
        HPBarText.text = HP.ToString();
    }

    public void TryToDie(int HP)
    {
        if (this.HP <= 0)
        {
            _dungeonMaster.AllActors.Remove(this);
            _dungeonMaster.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
            Destroy(gameObject);
        }
    }

    public void AddActionToFightHistory()
    {
        if (FightBasedActionHistory == null)
        {
            FightBasedActionHistory = new()
            {
                _dungeonMaster.CurrentAction
            };
        }
        else
        {
            FightBasedActionHistory.Add(_dungeonMaster.CurrentAction);
        }
    }

    public void TakeDamage(int damageToTake)
    {
        HP -= damageToTake;
    }
}