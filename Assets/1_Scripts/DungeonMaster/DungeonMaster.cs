using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    [SerializeField] private TurnMaster _turnMaster;
    public List<Cell> Cells;
    public List<IActor> AllActors;

    public List<IAction> MutualActionRow;
    public IAction CurrentAction;
    public IActor CurrentActor;

    public MonsterActor MonsterRefference;
    public PlayerActor PlayerActor;

    public Dictionary<IActor, IMonster> MonstersWithActorReference;

    private void Start()
    {
        MonstersWithActorReference = new();
        AllActors = new();
        CreateCellPositionsDictionary();

        MonsterRefference.Initialize();
        PlayerActor.Initialize();
    }

    public IEnumerator IterateThroughActionRow()
    {
        MutualActionRow = CreateMutualActionRow();
        InitializeCellIndexHistories();
        foreach (IAction action in MutualActionRow)
        {
            CurrentAction = action;
            CurrentActor = action.Actor;
            yield return ApplyTurnBasedStatusEffects();
            yield return CurrentAction.ExecuteAction(this);
            CurrentActor.AddActionToFightHistory();
        }   
        CurrentAction = null;
        CurrentActor = null;
    }

    private IEnumerator ApplyTurnBasedStatusEffects()
    {
        List<IStatusEffect> effectsToDestroy= new();

        foreach (IStatusEffect statusEffect in CurrentActor.StatusEffectsForTurn)
        {
            if (statusEffect.DestroyAfterApplication)
            {
                effectsToDestroy.Add(statusEffect);
            }
            Debug.Log($"Status effect {statusEffect} is applied");
            yield return statusEffect.ApplyStatusEffect(this);
        }
        foreach (IStatusEffect effect in effectsToDestroy)
        {
            effect.SelfDestroy(this);
        }
    }

    private List<IAction> CreateMutualActionRow()
    {
        List<IAction> row = new();
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

    private void InitializeCellIndexHistories()
    {
        foreach (IActor actor in AllActors)
        {
            actor.PositionCellIndexHistory = new();
            actor.PositionCellIndexHistory.Push(actor.PositionCellIndex);
        }
    }

    private void CreateCellPositionsDictionary()
    {
        Cells = new();
        foreach (Transform transform in MovementCellAnchors)
        {
            Cell cell = new()
            {
                CellPosition = transform.position,
                EnityOccupyingThisCell = null
            };
            Cells.Add(cell);
        }
    }
}