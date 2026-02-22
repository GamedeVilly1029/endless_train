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
        _actionMaster.GiveActionsToPlayerBelt();
        if (_dungeonMaster.MonsterRefference != null)
        {
            _actionMaster.GiveActionsToMonster1ActionRow
            (
                _dungeonMaster.MonsterRefference,
                _dungeonMaster.MonstersWithActorReference[_dungeonMaster.MonsterRefference]
            );
        }
    }

    private IEnumerator EndTurn()
    {
        yield return _dungeonMaster.StartCoroutine(_dungeonMaster.IterateThroughActionRow());
        StartTurn();
    }
}