using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnProcessor : MonoBehaviour
{
    [SerializeField] private TurnMaster _turnMaster;
    [SerializeField] private ActionMaster _actionMaster;
    [SerializeField] private LevelMaster _levelMaster;

    public IAction CurrentAction;
    public IActor CurrentActor;

    private bool _noActionsLeft;
    private List<IAction> _playerActions;

    public IEnumerator IterateThroughActionRow()
    {
        yield return BeforeIteration();
        yield return Iteration();
        AfterIteration();
    }

    private IEnumerator BeforeIteration()
    {
        PutPlayerOnTop();
        _playerActions = new();
        foreach (IActor actor in _levelMaster.AllActors)
        {
            actor.InitializeCellIndexHistories();
            yield return actor.RunBeforeTurnStatuses();
        }
        _noActionsLeft = false;
    }

    private IEnumerator ProcessAction(IActor actor)
    {
        CurrentActor = actor;
        yield return actor.TriggerTurnBasedStatusEffects();
        if (actor is PlayerActor)
        {
        _playerActions.Add(CurrentAction.PrototypeAction);
        }
        yield return CurrentAction.ExecuteAction();
        actor.AddActionToFightHistory();
    }

    private IEnumerator Iteration()
    {
        while (!_noActionsLeft)
        {
            foreach (IActor actor in _levelMaster.AllActors)
            {
                CurrentAction = actor.ReturnFirstActionInRow();
                if (CurrentAction != null)
                {
                    yield return ProcessAction(actor);
                }
            }

            _noActionsLeft = true;
            foreach (IActor actor in _levelMaster.AllActors)
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

        PlayerBeltPatternPicker cooldownDecreaser = _levelMaster.Player.PatternPicker as PlayerBeltPatternPicker;
        cooldownDecreaser.DecreaseCooldown(_playerActions);
        RemoveDeadActors();
    }

    private void PutPlayerOnTop()
    {
        _levelMaster.AllActors.Remove(_levelMaster.Player);
        _levelMaster.AllActors.Insert(0, _levelMaster.Player);
    }

    private void RemoveDeadActors()
    {
        if (_levelMaster.AllActors.Count == 0)
        {
            return;
        }
        for (int i = _levelMaster.AllActors.Count - 1; i >= 0; i--)
        {
            if (_levelMaster.AllActors[i].IsDead)
            {
                Destroy(_levelMaster.AllActors[i] as BaseActor);
                _levelMaster.AllActors.RemoveAt(i);
            }
        }
    }
}