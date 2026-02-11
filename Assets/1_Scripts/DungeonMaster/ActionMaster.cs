using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private GameObject _movementActionUIPrefab;
    [SerializeField] private GameObject _attackActionUIPrefab;
    public List<Action> PlayerActionPrototypes;
    public Dictionary<IActor, List<Action>> AllMonsterActionPrototypes;

    private Dictionary<MonsterTypes, Dictionary<Action, int>> MonsterActionReferencesByType;

    private void Start()
    {
        CreatePlayerActionsPrototype();
        CreateMonsterActionsPrototype();
    }

    public void GiveActionsToPlayerBelt()
    {
        _dungeonMaster.PlayerActor.ActionRow.Clear();

        foreach (Action actionWithUI in _dungeonMaster.PlayerActor.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _dungeonMaster.PlayerActor.Belt.Clear();

        for (int i = 0; i < PlayerActionPrototypes.Count; i++)
        {
            Action action = PlayerActionPrototypes[i].CloneAndInstantiateUI(_dungeonMaster.PlayerActor.BeltPanel);
            _dungeonMaster.PlayerActor.Belt.Add(action);
        }
    }

    public void GiveActionsToMonster1ActionRow()
    {
        // _dungeonMaster.MonsterRefference.ActionRow.Clear();

        // for (int i = 0; i < Monster1ActionPrototypes.Count; i++)
        // {
        //     Action action = Monster1ActionPrototypes[i].CloneWithoutUI();
        //     _dungeonMaster.MonsterRefference.ActionRow.Add(action);
        // }
    }

    private void CreatePlayerActionsPrototype()
    {
        PlayerActionPrototypes = new();
        PlayerActionPrototypes.Add(CreatePlayerAction(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0));
        PlayerActionPrototypes.Add(CreatePlayerAction(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0));
        PlayerActionPrototypes.Add(CreatePlayerAction(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0));
        PlayerActionPrototypes.Add(CreatePlayerAction(ActionConcretes.AttackEntityAhead, _attackActionUIPrefab, 5));
    }

    private void CreateMonsterActionsPrototype()
    {
        AllMonsterActionPrototypes = new();
        foreach (IActor monster in _dungeonMaster.AllMonsters)
        {
            AllMonsterActionPrototypes.Add(monster, new List<Action>());
        }
    }

    private Action CreatePlayerAction(Func<DungeonMaster, IEnumerator> concrete, GameObject UIRepresentationPrefab, int valueForActionConcrete)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.Concrete = concrete;
        action.ActionConstruct.Add(constructElement);

        action.ValueForActionConcrete = valueForActionConcrete;

        action.UIRepresentation = UIRepresentationPrefab;
        action.Actor = _dungeonMaster.PlayerActor;
        return action;
    }

    private Action CreateMonsterAction(Func<DungeonMaster, IEnumerator> concrete, IActor monster, int valueForActionConcrete)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.Concrete = concrete;
        action.ActionConstruct.Add(constructElement);

        action.ValueForActionConcrete = valueForActionConcrete;

        action.Actor = monster;
        return action;
    }

    private void CreateMonsterTypeReferenceSet()
    {
        MonsterActionReferencesByType = new();

        Dictionary<Action, int> Glist1ActionReferenceSet = new();
        // Glist1ActionReferenceSet.Add();
    }
}