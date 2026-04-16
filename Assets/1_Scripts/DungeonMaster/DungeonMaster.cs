using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    [SerializeField] private TurnMaster _turnMaster;
    [SerializeField] private ActionMaster _actionMaster;

    [HideInInspector] public PlayerActor Player;
    public List<BaseActor> AllActors;

    public IAction CurrentAction;
    public IActor CurrentActor;

    [HideInInspector] public List<Cell> Cells;

    private bool _noActionsLeft;
    private List<IAction> _playerActions;

    private void Start()
    {
        CreateCellPositionsDictionary();
        InitializeActors();
    }

    public IEnumerator IterateThroughActionRow()
    {
        BeforeIteration();
        yield return Iteration();
        AfterIteration();
    }

    private void BeforeIteration()
    {
        PutPlayerOnTop();
        _playerActions = new();
        foreach (IActor actor in AllActors)
        {
            actor.InitializeCellIndexHistories();
        }
        _noActionsLeft = false;
    }

    private IEnumerator ProcessAction(IActor actor)
    {
        CurrentActor = actor;
        yield return actor.TriggerTurnBasedStatusEffects();
        if (actor is PlayerActor)
        {
        _playerActions.Add(CurrentAction.ActionCloneReference);
        }
        yield return CurrentAction.ExecuteAction(this);
        actor.AddActionToFightHistory();
    }

    private IEnumerator Iteration()
    {
        while (!_noActionsLeft)
        {
            foreach (IActor actor in AllActors)
            {
                CurrentAction = actor.ReturnFirstActionInRow();
                if (CurrentAction != null)
                {
                    yield return ProcessAction(actor);
                }
            }

            _noActionsLeft = true;
            foreach (IActor actor in AllActors)
            {
                if (actor.ReturnFirstActionInRow() != null)
                {
                    _noActionsLeft = false;
                }
            }
        }
    }

    private void AfterIteration()
    {
        CurrentAction = null;
        CurrentActor = null;

        PlayerBeltPatternPicker cooldownDecreaser = Player.PatternPicker as PlayerBeltPatternPicker;
        cooldownDecreaser.DecreaseCooldown(_playerActions);
    }

    private void PutPlayerOnTop()
    {
        AllActors.Remove(Player);
        AllActors.Insert(0, Player);
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

    private void InitializeActors()
    {
        foreach (IActor actor in AllActors)
        {
            actor.Initialize();
            if (actor is PlayerActor)
            {
                Player = actor as PlayerActor;
            }
        }
        if (Player == null)
        {
            Debug.Log("Player actor is null, fix that ;3");
        }
    }
}