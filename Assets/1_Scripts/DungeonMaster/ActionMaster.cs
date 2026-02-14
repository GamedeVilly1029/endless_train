using UnityEngine;
using System.Collections.Generic;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private GameObject _movementActionUIPrefab;
    [SerializeField] private GameObject _attackActionUIPrefab;
    private List<Action> _playerActionPrototypes;
    private Dictionary<MonsterTypes, List<Action>> _monsterActionReferencesByType;

    private void Start()
    {
        CreatePlayerActionsPrototype();
        CreateMonsterTypeReferenceSet();
    }

    public void GiveActionsToPlayerBelt()
    {
        _dungeonMaster.PlayerActor.ActionRow.Clear();

        foreach (Action actionWithUI in _dungeonMaster.PlayerActor.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _dungeonMaster.PlayerActor.Belt.Clear();

        for (int i = 0; i < _playerActionPrototypes.Count; i++)
        {
            Action action = _playerActionPrototypes[i].CloneAndInstantiateUI(_dungeonMaster.PlayerActor.BeltPanel);
            action.Actor = _dungeonMaster.PlayerActor;
            _dungeonMaster.PlayerActor.Belt.Add(action);
        }
    }

    public void GiveActionsToMonster1ActionRow(IActor monsterActor, IMonster monster)
    {
        monsterActor.ActionRow.Clear();
        for (int i = 0; i < _monsterActionReferencesByType[monster.MonsterType].Count; i++)
        {
            Action action = _monsterActionReferencesByType[monster.MonsterType][i].CloneAndInstantiateUI(monsterActor.ActionRowPanel);
            action.Actor = monsterActor;
            monsterActor.ActionRow.Add(action);
        }
    }

    private void CreatePlayerActionsPrototype()
    {
        _playerActionPrototypes = new()
        {
            StaticActionFunctionality.CreateActionWithUI(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0),
            StaticActionFunctionality.CreateActionWithUI(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0),
            StaticActionFunctionality.CreateActionWithUI(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab, 0),
            StaticActionFunctionality.CreateActionWithUI(ActionConcretes.AttackEntityAhead, _attackActionUIPrefab, 5)
        };
    }

    private void CreateMonsterTypeReferenceSet()
    {
        _monsterActionReferencesByType = new();

        List<Action> Glist1ActionReferenceSet = new()
        {
            StaticActionFunctionality.CreateActionWithUI(ActionConcretes.MoveOneCellForward, _movementActionUIPrefab,0)
        };
        _monsterActionReferencesByType.Add(MonsterTypes.glist1, Glist1ActionReferenceSet);
    }
}