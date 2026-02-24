using UnityEngine;
using System.Collections.Generic;
using System;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    private List<IAction> _playerActionPrototypes;
    private Dictionary<MonsterTypes, List<IAction>> _monsterActionReferencesByType;

    private void Start()
    {
        CreatePlayerActionsPrototype();
        CreateMonsterTypeReferenceSet();
    }

    public void GiveActionsToPlayerBelt()
    {
        _dungeonMaster.PlayerActor.ActionRow.Clear();

        foreach (IAction actionWithUI in _dungeonMaster.PlayerActor.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _dungeonMaster.PlayerActor.Belt.Clear();

        for (int i = 0; i < _playerActionPrototypes.Count; i++)
        {
            IAction action = _playerActionPrototypes[i].CloneAndInstantiateUI(_dungeonMaster.PlayerActor.BeltPanel);
            action.Actor = _dungeonMaster.PlayerActor;
            _dungeonMaster.PlayerActor.Belt.Add(action);
        }
    }

    public void GiveActionsToMonster1ActionRow(IActor monsterActor, IMonster monster)
    {
        monsterActor.ActionRow.Clear();
        for (int i = 0; i < _monsterActionReferencesByType[monster.MonsterType].Count; i++)
        {
            IAction action = _monsterActionReferencesByType[monster.MonsterType][i].CloneAndInstantiateUI(monsterActor.ActionRowPanel);
            action.Actor = monsterActor;
            monsterActor.ActionRow.Add(action);
        }
    }

    private void CreatePlayerActionsPrototype()
    {
        _playerActionPrototypes = new();
        IAction strikeAction1 = new Strike();
        strikeAction1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(strikeAction1);

        IAction moveOneTileForward1 = new MoveOneTileForward();
        moveOneTileForward1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(moveOneTileForward1);

        IAction rotate1 = new Rotate();
        rotate1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(rotate1);

        IAction push1 = new Push();
        push1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(push1);

        IAction kneeDash1 = new KneeDash();
        kneeDash1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(kneeDash1);

        IAction heavyWalk1 = new HeavyWalk();
        heavyWalk1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(heavyWalk1);

        IAction angryRoar1 = new AngryRoar();
        angryRoar1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(angryRoar1);

        IAction tantrum1 = new Tantrum();
        tantrum1.InitializeAction(_dungeonMaster.PlayerActor, _dungeonMaster);
        _playerActionPrototypes.Add(tantrum1);
    }

    private void CreateMonsterTypeReferenceSet()
    {
        _monsterActionReferencesByType = new();

        List<IAction> Glist1ActionReferenceSet = CreateActionsForGlist1();
        _monsterActionReferencesByType.Add(MonsterTypes.glist1, Glist1ActionReferenceSet);
    }

    private List<IAction> CreateActionsForGlist1()
    {
        List<IAction> actions = new();
        // IAction moveOneTileForward1 = new MoveOneTileForward();
        // moveOneTileForward1.InitializeAction(_dungeonMaster.MonsterRefference, _dungeonMaster);
        // actions.Add(moveOneTileForward1);

        IAction strike1 = new Strike();
        strike1.InitializeAction(_dungeonMaster.MonsterRefference, _dungeonMaster);
        actions.Add(strike1);

        return actions; 
    }
}