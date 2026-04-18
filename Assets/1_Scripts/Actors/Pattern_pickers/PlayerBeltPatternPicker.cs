using System.Collections.Generic;
using UnityEngine;

public class PlayerBeltPatternPicker : BasePatternPicker 
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    public List<IAction> PlayerActionPrototypes;

        
    public override void FillActionRowOrBelt()
    {
        foreach (IAction actionWithUI in _dungeonMaster.Player.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _dungeonMaster.Player.Belt.Clear();
        _dungeonMaster.Player.ActionRowInst.Actions.Clear();

        for (int i = 0; i < PlayerActionPrototypes.Count; i++)
        {
            if (PlayerActionPrototypes[i].Cooldown > 0)
            {
                continue;
            }
            else
            {
                IAction action = PlayerActionPrototypes[i].CloneAndInstantiateUI(_dungeonMaster.Player.BeltPanel, PlayerActionPrototypes[i]);
                action.Actor = _dungeonMaster.Player;
                _dungeonMaster.Player.Belt.Add(action);
            }
        }
    }

    public override void InitializeActionPrototypes()
    {
        PlayerActionPrototypes = CreatePlayerActionsPrototype();
    }

    private List<IAction> CreatePlayerActionsPrototype()
    {
        List<IAction> actions = new();
        IAction strikeAction1 = new Strike();
        strikeAction1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(strikeAction1);

        IAction moveOneTileForward1 = new MoveOneTileForward();
        moveOneTileForward1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(moveOneTileForward1);

        IAction moveOneTileBackwards = new MoveOneTileBackwards();
        moveOneTileBackwards.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(moveOneTileBackwards);

        IAction rotate1 = new Rotate();
        rotate1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(rotate1);

        IAction push1 = new Push();
        push1.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(push1);

        IAction dash = new Dash();
        dash.InitializeAction(_dungeonMaster.Player, _dungeonMaster);
        actions.Add(dash);

        return actions;
    }

    public void DecreaseCooldown(List<IAction> exceptions)
    {
        foreach (IAction exception in exceptions)
        {
            foreach (IAction prototypeAction in PlayerActionPrototypes)
            {
                if (exceptions.Contains(prototypeAction))
                {
                    continue;
                }
                else
                {
                    if (prototypeAction.Cooldown > 0)
                    {
                        prototypeAction.Cooldown -= 1;
                    }
                }
            }
        }
    }
}