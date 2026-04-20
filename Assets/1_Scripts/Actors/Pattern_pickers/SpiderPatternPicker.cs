using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatternPicker : BasePatternPicker
{
    public Spider SpiderInstance;
    public DungeonMaster DungeonMasterInst;

    private List<IAction> _approach;
    private List<IAction> _rotate;
    private List<IAction> _sneakyLeap;
    private List<IAction> _stunningShout;

    public override void FillActionRowOrBelt()
    {
        SpiderInstance.ActionRowInst.Actions.Clear();

        Func<float, float, float, bool> HPmoreThan30Percent = HPBasedCondition.CurrentHPIsMoreThanXPercent;
        Func<DungeonMaster, IActor, IActor, int, bool> PlayerInRange = CellBasedCondition.ActorInRangeOfXCellsFromOtherActor;
        Func<DungeonMaster, IActor, IActor, bool> PlayerAhead = CellBasedCondition.ActorIsOnCellsAhead;

        if (!PlayerAhead(DungeonMasterInst, DungeonMasterInst.Player, SpiderInstance))
        {
            SpiderInstance.ActionRowInst.Actions = CopyActionSet(_rotate, SpiderInstance.ActionRowInst.Panel);
            return;
        }

        if (!PlayerInRange(DungeonMasterInst, DungeonMasterInst.Player, SpiderInstance, 2))
        {
            SpiderInstance.ActionRowInst.Actions = CopyActionSet(_approach, SpiderInstance.ActionRowInst.Panel);
            return;
        }

        if (HPmoreThan30Percent(SpiderInstance.MaxHP, SpiderInstance.CurrentHP, 30))
        {
            int randomInt = UnityEngine.Random.Range(1, 4);
            if (randomInt == 1)
            {
                SpiderInstance.ActionRowInst.Actions = CopyActionSet(_sneakyLeap, SpiderInstance.ActionRowInst.Panel);
                return;
            }
            else
            {
                SpiderInstance.ActionRowInst.Actions = CopyActionSet(_stunningShout, SpiderInstance.ActionRowInst.Panel);
                return;
            }
            
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        SpiderInstance.ActionRowInst.Actions = null;
        return;
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
        move.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(move);

        IAction move1 = new MoveOneTileForward();
        move1.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(move1);

        return actions;
    }


    private List<IAction> InitializeRotate()
    {
        List<IAction> actions = new();

        IAction rotate = new Rotate();
        rotate.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(rotate);

        return actions;
    }

    private List<IAction> InitializeSneakyLeap()
    {
        List<IAction> actions = new();
        IAction dash = new Dash();
        dash.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(dash);

        IAction strike = new Strike();
        strike.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(strike);

        IAction moveBack = new MoveOneTileBackwards();
        moveBack.InitializeAction(SpiderInstance, DungeonMasterInst);
        actions.Add(moveBack);

        return actions;
    }

    private List<IAction> InitializeStunningShout()
    {
        List<IAction> actions = new();

        IAction stunFirstNextTurn = new StunFirstPlayerActionNextTurn();
        stunFirstNextTurn.InitializeAction(SpiderInstance, DungeonMasterInst);
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