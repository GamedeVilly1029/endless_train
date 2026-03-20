using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseActor : MonoBehaviour, IActor
{
    public DungeonMaster DungeonMasterInstance;
    public RectTransform ActionRowPanelInstance;
    public TextMeshPro HPBarText;
    public SpriteRenderer SpriteRend;
    public ActionRow ActionRowSetter;

    public Transform Transform{get{return transform;}set{}}
    public Transform GraphicTransform;
    public ActionRow ActionRowInst {get{return ActionRowSetter;}set{}}
    public int MaxHP{get;set;}
    public int CurrentHP{get;set;}
    public int PositionCellIndex {get;set;}
    public Stack<int> PositionCellIndexHistory{get;set;}
    public bool IsFacingRight {get;set;}
    public List<IStatusEffect> StatusEffectsForTurn {get;set;}
    public List<IAction> FightBasedActionHistory{get;set;}
    public List<IStatusEffect> StatusEffectsBeforeTakingDamage {get;set;}

    public void Initialize()
    {
        DungeonMasterInstance.AllActors.Add(this);
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 99;
        CurrentHP = MaxHP;
    }


    private void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
        SpriteRend.flipX = !IsFacingRight;
    }

    public void TryToDie(int HP)
    {
        if (CurrentHP <= 0)
        {
            DungeonMasterInstance.AllActors.Remove(this);
            DungeonMasterInstance.Cells[PositionCellIndex].EnityOccupyingThisCell = null;
            Destroy(gameObject);
        }
    }

    public void AddActionToFightHistory()
    {
        if (FightBasedActionHistory == null)
        {
            FightBasedActionHistory = new()
            {
                DungeonMasterInstance.CurrentAction
            };
        }
        else
        {
            FightBasedActionHistory.Add(DungeonMasterInstance.CurrentAction);
        }
    }

    public IEnumerator RunBeforeDamageStatuses()
    {
        foreach (IStatusEffect statusEffect in StatusEffectsBeforeTakingDamage)
        {
            yield return statusEffect.ApplyStatusEffect(DungeonMasterInstance);
        }
    }

    public IEnumerator SubtractDamageFromHP(int damageToTake)
    {
        yield return CurrentHP -= damageToTake;
    }
}