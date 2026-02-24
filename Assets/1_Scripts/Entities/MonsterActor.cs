using System.Collections;
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
    public List<IStatusEffect> StatusEffectsForTurn {get;set;}
    public List<IAction> FightBasedActionHistory{get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTakingDamage {get;set;}

    public void Initialize()
    {
        _dungeonMaster.MonstersWithActorReference.Add(this, this);
        _dungeonMaster.AllActors.Add(this);
        Transform.position = _dungeonMaster.Cells[1].CellPosition;
        _dungeonMaster.Cells[1].EnityOccupyingThisCell = this;
        PositionCellIndex = 1;
        IsFacingRight = false;
        MonsterType = MonsterTypes.glist1;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        HP = 99;
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
            _dungeonMaster.AllActors.Remove(this);
            _dungeonMaster.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
            Destroy(gameObject);
        }
    }

    public void AddActionToFightHistory()
    {
        FightBasedActionHistory = new();
    }

    public IEnumerator TakeDamage(int damageToTake)
    {
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTakingDamage)
        {
            yield return statusEffect.ApplyStatusEffect(_dungeonMaster);
        }
        HP -= damageToTake;
    }
}