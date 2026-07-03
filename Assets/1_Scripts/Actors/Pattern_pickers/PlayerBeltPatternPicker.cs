using System.Collections.Generic;
using UnityEngine;

public class PlayerBeltPatternPicker : BasePatternPicker
{
    public List<BaseAction> PlayerActionPrototypes;
    public List<int> AdditionalActionIndexes;

    public override void ChildFillActionRowOrBelt()
    {
        foreach (BaseAction actionWithUI in _levelMaster.Player.Belt)
        {
            Destroy(actionWithUI.UIRepresentation);
        }
        _levelMaster.Player.Belt.Clear();
        _levelMaster.Player.ActionRowInst.Actions.Clear();

        for (int i = 0; i < PlayerActionPrototypes.Count; i++)
        {
            if (PlayerActionPrototypes[i].Cooldown > 0)
            {
                continue;
            }
            else
            {
                BaseAction action = PlayerActionPrototypes[i].CloneAndInstantiateUI(_levelMaster.Player.BeltPanel, PlayerActionPrototypes[i]); action.Actor = _levelMaster.Player;
                _levelMaster.Player.Belt.Add(action);
            }
        }
    }

    public override void InitializeChild()
    {
        PlayerActionPrototypes = CreatePlayerActionsPrototype();
    }

    private List<BaseAction> CreatePlayerActionsPrototype()
    {
        List<BaseAction> actions = new();
        BaseAction strikeAction1 = new Strike();
        strikeAction1.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strikeAction1);

        BaseAction moveOneTileForward1 = new MoveOneTileForward();
        moveOneTileForward1.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkForward");
        actions.Add(moveOneTileForward1);

        BaseAction moveOneTileBackwards = new MoveOneTileBackwards();
        moveOneTileBackwards.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(moveOneTileBackwards);

        BaseAction rotate1 = new Rotate();
        rotate1.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "MovementActionUI/Rotate");
        actions.Add(rotate1);

        BaseAction push1 = new Push();
        push1.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "PushActionUI/Push");
        actions.Add(push1);

        BaseAction basicDefend = new BasicDefend();
        basicDefend.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "DefenseActionUI/BasicDefend");
        actions.Add(basicDefend);

        LoadAdditionalActions(actions);

        return actions;
    }

    public void LoadAdditionalActions(List<BaseAction> actions)
    {
        foreach (int idx in AdditionalActionIndexes)
        {
            if (idx == 0)
            {
                BaseAction strikeBack = new StrikeBackwards();
                strikeBack.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "AttackActionUI/KickBack");
                actions.Add(strikeBack);
            }
            else if (idx == 1)
            {
                BaseAction sling = new Slingshot();
                sling.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "AttackActionUI/SlingShot");
                actions.Add(sling);
            }
            else if (idx == 2)
            {
                BaseAction kickpush = new KickPush();
                kickpush.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "AttackActionUI/KneeKick");
                actions.Add(kickpush);
            }
            else if (idx == 3)
            {
                BaseAction legKick = new LegKick();
                legKick.Initialize(_levelMaster.Player, _turnProcessor, _levelMaster, 0, "AttackActionUI/Kick");
                actions.Add(legKick);
            }
        }
    }

    public void DecreaseCooldown(List<BaseAction> exceptions)
    {
        foreach (BaseAction exception in exceptions)
        {
            foreach (BaseAction prototypeAction in PlayerActionPrototypes)
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