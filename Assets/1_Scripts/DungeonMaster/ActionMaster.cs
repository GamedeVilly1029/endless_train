using UnityEngine;
using System.Collections.Generic;
using System;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private MechanicPatternPicker _mechanicPatternPicker;
    private List<IAction> _playerActionPrototypes;

    private void Start()
    {
        CreatePlayerActionsPrototype();
        _mechanicPatternPicker.InitializeActionPatterns();
    }

    public void GiveActionsToPlayerBelt()
    {
        _dungeonMaster.Player.ActionRow.Clear();

        foreach (IAction actionWithUI in _dungeonMaster.Player.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _dungeonMaster.Player.Belt.Clear();

        for (int i = 0; i < _playerActionPrototypes.Count; i++)
        {
            IAction action = _playerActionPrototypes[i].CloneAndInstantiateUI(_dungeonMaster.Player.BeltPanel);
            action.Actor = _dungeonMaster.Player;
            _dungeonMaster.Player.Belt.Add(action);
        }
    }

    public void GiveActionsToMechanic()
    {
        _dungeonMaster.Mechanic.ActionRow.Clear();
        _dungeonMaster.Mechanic.ActionRow = _mechanicPatternPicker.ReturnPattern();
    }

    private void CreatePlayerActionsPrototype()
    {
        _playerActionPrototypes = new();
        IAction strikeAction1 = new Strike();
        strikeAction1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(strikeAction1);

        IAction moveOneTileForward1 = new MoveOneTileForward();
        moveOneTileForward1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(moveOneTileForward1);

        IAction rotate1 = new Rotate();
        rotate1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(rotate1);

        IAction push1 = new Push();
        push1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(push1);

        IAction kneeDash1 = new KneeDash();
        kneeDash1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(kneeDash1);

        IAction heavyWalk1 = new HeavyWalk();
        heavyWalk1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(heavyWalk1);

        IAction angryRoar1 = new AngryRoar();
        angryRoar1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(angryRoar1);

        IAction tantrum1 = new Tantrum();
        tantrum1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        _playerActionPrototypes.Add(tantrum1);
    }
}