using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurnMaster : MonoBehaviour
{
    [SerializeField] private TurnProcessor _turnProcessor;
    [SerializeField] private LevelMaster _levelMaster;
    [SerializeField] private UIMaster _uIMaster;
    public UnityEvent OnEndTurn = new();
    public UnityEvent OnStartTurn = new();
    public int TurnNumber;

    private void Awake()
    {
        OnStartTurn.AddListener(() => StartTurn());
        OnEndTurn.AddListener(() => StartCoroutine(EndTurn()));
        TurnNumber = 0;

        _uIMaster.NewFightUIStarterInst.OnStartFightButtonClick.AddListener(() => StartTurnLoadRoom());
    }

    private void Start()
    {
        StartTurn();
    }

    private void StartTurn()
    {
        if (!_levelMaster.AliveEnemiesPresented())
        {
            EndFight();
        }
        else
        {
            TurnNumber += 1;

            foreach (BaseActor actor in _levelMaster.AllActors)
            {
                actor.PatternPicker.FillActionRowOrBelt();
            }

            _levelMaster.Player.ActionInRowCount = 0;
        }
    }

    private void StartTurnLoadRoom()
    {
        _levelMaster.LoadNextRoom();
        StartTurn();
    }

    private void EndFight()
    {
        Debug.Log("Fight ended - displaying 'Start new fight button'");
        _uIMaster.PlayerUIManagerInst.TurnUIOff();
        _uIMaster.NewFightUIStarterInst.StartFightButtonAppear();
    }

    private IEnumerator EndTurn()
    {
        yield return _turnProcessor.StartCoroutine(_turnProcessor.ProcessTurn());
        StartTurn();
    }
}