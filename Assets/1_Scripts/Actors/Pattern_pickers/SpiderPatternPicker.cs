using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatternPicker : BasePatternPicker
{
    private List<IAction> _approach;
    private List<IAction> _rotate;
    private List<IAction> _sneakyLeap;
    private List<IAction> _stunningShout;

    public override void FillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        if (!new ActorIsOnCellsAheadCondition(_turnProcessor, _levelMaster, _levelMaster.Player, _actor).Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_rotate, _actor.ActionRowInst.Panel);
            return;
        }

        if (!new ActorInRangeOfXCellsFromOtherActorCondition(_turnProcessor, _levelMaster, _levelMaster.Player, _actor, 2).Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_approach, _actor.ActionRowInst.Panel);
            return;
        }

        // if (new CurrentHPIsMoreThanXPercentCondition(_turnProcessor, _levelMaster, SpiderInstance.MaxHP, SpiderInstance.CurrentHP, 30f).Execute())
        // {
            int randomInt = UnityEngine.Random.Range(1, 4);
            if (randomInt == 1)
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_sneakyLeap, _actor.ActionRowInst.Panel);
                return;
            }
            else
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_stunningShout, _actor.ActionRowInst.Panel);
                return;
            }
        // }

        // Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        // SpiderInstance.ActionRowInst.Actions = null;
        // return;
    }

    public override void InitializeActionPrototypes()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _sneakyLeap = InitializeSneakyLeap();
        _stunningShout = InitializeStunningShout();
    }

    private List<IAction> InitializeApproach()
    {
        List<IAction> actions = new();

        IAction move = new MoveOneTileForward();
        move.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(move);

        IAction move1 = new MoveOneTileForward();
        move1.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(move1);

        return actions;
    }


    private List<IAction> InitializeRotate()
    {
        List<IAction> actions = new();

        IAction rotate = new Rotate();
        rotate.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(rotate);

        return actions;
    }

    private List<IAction> InitializeSneakyLeap()
    {
        List<IAction> actions = new();
        IAction dash = new Dash();
        dash.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(dash);

        IAction strike = new Strike();
        strike.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(strike);

        IAction moveBack = new MoveOneTileBackwards();
        moveBack.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(moveBack);

        return actions;
    }

    private List<IAction> InitializeStunningShout()
    {
        List<IAction> actions = new();

        IAction stunFirstNextTurn = new StunFirstPlayerActionNextTurn();
        stunFirstNextTurn.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(stunFirstNextTurn);

        return actions;
    }

    private List<IAction> CopyActionSet(List<IAction> set, RectTransform UIPanel)
    {
        List<IAction> copies = new();
        foreach (IAction action in set)
        {
            IAction copy = action.CloneAndInstantiateUI(UIPanel, action);
            copies.Add(copy);
        }
        return copies;
    }
}