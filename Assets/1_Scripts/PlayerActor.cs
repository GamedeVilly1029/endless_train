using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster _master;
    [SerializeField] private GameObject _movementActionUIPrefab;
    [SerializeField] private GameObject _attackActionUIPrefab;

    public Transform Transform{get{return transform;}set{}}

    public List<Action> ActionRow{get;set;} = new();
    public Transform ActionRowPanel;
    public List<Action> Belt = new();
    public Transform BeltPanel;

    private void Start()
    {
        Transform.position = _master.Cells[0].CellPosition;
        _master.Cells[0].IsOcupiedByEntity = true;
        Belt.Add(CreateAction(ActionConcretes.WalkXTiles(_master, 1), _movementActionUIPrefab));
        Belt.Add(CreateAction(ActionConcretes.AttackEntityAhead(_master, 2), _attackActionUIPrefab));
    }

    private Action CreateAction(IEnumerator concrete, GameObject UIRepresentationPrefab)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.ConcreteCoroutine = concrete;
        action.ActionConstruct.Add(constructElement);

        action.UIRepresentation = Instantiate(UIRepresentationPrefab, BeltPanel.transform);
        action.Actor = this;
        return action;
    }
}