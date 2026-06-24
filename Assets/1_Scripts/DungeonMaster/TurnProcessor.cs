using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnProcessor : MonoBehaviour
{
    [SerializeField] private TurnMaster _turnMaster;
    [SerializeField] private LevelMaster _levelMaster;

    public BaseAction CurrentAction;
    public BaseActor CurrentActor;

    private Queue<BaseActor> _enemies;
    private List<BaseAction> _playerActions;

    public IEnumerator ProcessTurn()
    {
        yield return BeforeIteration();
        yield return Iteration();
        yield return AfterIteration();
    }

    private IEnumerator BeforeIteration() 
    {
        _playerActions = new();
        foreach (BaseActor actor in _levelMaster.AllActors)
        {
            yield return actor.TurnStart();
        }
    }

    private IEnumerator ProcessAction(BaseActor actor)
    {
        CurrentActor = actor;
        CurrentAction = actor.ReturnFirstActionInRow();

        yield return actor.RunDuringTurnStatuses();
        if (actor is PlayerActor)
        {
            _playerActions.Add(CurrentAction.PrototypeAction);
        }
        yield return CurrentAction.ExecuteAction();
        actor.AddActionToFightHistory();
    }

    private IEnumerator Iteration()
    {
        for (int i = 0; i < _levelMaster.AllActors.Count; i++)
        {
            if (_levelMaster.AllActors[i].ActionRowInst.CanExecuteActions())
            {
                _enemies = CreateEnemies();
                yield return RunOneIteration(_enemies);
                i = -1;
            }
        }
    }

    private IEnumerator RunOneIteration(Queue<BaseActor> enemies)
    {
        while (enemies.Count > 0)
        {
            BaseActor enemy = enemies.Dequeue();
            if (enemy.IsDead)
            {
                continue;
            }
            if (_levelMaster.Player.ActionRowInst.CanExecuteActions())
            {
                yield return ProcessAction(_levelMaster.Player);
            }
            if (enemy.ActionRowInst.CanExecuteActions())
            {
                yield return ProcessAction(enemy);
            }
        }
        RemoveDeadActors();
    }

    private IEnumerator AfterIteration()
    {
        CurrentAction = null;
        CurrentActor = null;

        PlayerBeltPatternPicker cooldownDecreaser = _levelMaster.Player.PatternPicker as PlayerBeltPatternPicker;
        cooldownDecreaser.DecreaseCooldown(_playerActions);
        RemoveDeadActors();

        foreach (BaseActor actor in _levelMaster.AllActors)
        {
            yield return actor.TurnEnd();
        }
    }

    private Queue<BaseActor> CreateEnemies()
    {
        Queue<BaseActor> enemies = new();
        foreach (BaseActor actor in _levelMaster.AllActors)
        {
            if (actor is PlayerActor)
            {
                continue;
            }
            else
            {
                enemies.Enqueue(actor);
            }
        }
        return enemies;
    }

    private void RemoveDeadActors()
    {
        if (_levelMaster.AllActors.Count == 0)
        {
            return;
        }
        for (int i = _levelMaster.AllActors.Count - 1; i >= 0; i--)
        {
            if (_levelMaster.AllActors[i].ShouldBeDestroyed)
            {
                Destroy(_levelMaster.AllActors[i].gameObject);
                _levelMaster.AllActors.RemoveAt(i);
            }
        }
    }
}