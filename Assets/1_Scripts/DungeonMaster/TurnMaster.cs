using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurnMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private ActionMaster _actionMaster;
    public UnityEvent OnEndTurn = new();
    public UnityEvent OnStartTurn = new();
    public int TurnNumber;

    private void Awake()
    {
        OnStartTurn.AddListener(() => StartTurn());
        OnEndTurn.AddListener(() => StartCoroutine(EndTurn()));
        TurnNumber = 0;
    }

    private void Start()
    {
        StartTurn();
    }

    private void StartTurn()
    {
        TurnNumber += 1;

        foreach (IActor actor in _dungeonMaster.AllActors)
        {
            actor.PatternPicker.FillActionRowOrBelt();
        }

        _dungeonMaster.Player.ActionInRowCount = 0;
    }

    private IEnumerator EndTurn()
    {
        yield return _dungeonMaster.StartCoroutine(_dungeonMaster.IterateThroughActionRow());
        StartTurn();
    }
}