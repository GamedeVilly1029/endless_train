using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mechanic : MonoBehaviour, IActor, IMonster
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private TextMeshPro HPBarText;
    [SerializeField] private RectTransform _actionRowPanel;

    public int MaxHP{get;set;}
    public int CurrentHP{get;set;}
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
        Transform.position = _dungeonMaster.Cells[9].CellPosition;
        _dungeonMaster.Cells[9].EnityOccupyingThisCell = this;
        PositionCellIndex = 9;
        IsFacingRight = true;
        MonsterType = MonsterTypes.glist1;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 99;
        CurrentHP = MaxHP;
    }

    private void Update()
    {
        HPBarText.text = CurrentHP.ToString();
        TryToDie(CurrentHP);
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

    public IEnumerator RunBeforeDamageStatuses()
    {
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTakingDamage)
        {
            yield return statusEffect.ApplyStatusEffect(_dungeonMaster);
        }
    }

    public IEnumerator SubtractDamageFromHP(int damageToTake)
    {
        yield return CurrentHP -= damageToTake;
    }
}