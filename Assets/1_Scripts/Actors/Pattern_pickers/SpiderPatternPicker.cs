using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatternPicker : BasePatternPicker
{
    public Spider SpiderInstance;
    public DungeonMaster DungeonMasterInst;

    public List<IAction> approach;
    public List<IAction> rotate;
    public List<IAction> sneakyLeap;

    public override void FillActionRowOrBelt()
    {
        SpiderInstance.ActionRowInst.Actions.Clear();

        Func<float, float, float, bool> HPmoreThan30Percent = HPBasedCondition.CurrentHPIsMoreThanXPercent;
        Func<DungeonMaster, IActor, IActor, int, bool> PlayerInRange = CellBasedCondition.ActorInRangeOfXCellsFromOtherActor;
        Func<DungeonMaster, IActor, IActor, bool> PlayerAhead = CellBasedCondition.ActorIsOnCellsAhead;

        if (!PlayerAhead(DungeonMasterInst, DungeonMasterInst.Player, SpiderInstance))
        {
            SpiderInstance.ActionRowInst.Actions = CopyActionSet(rotate, SpiderInstance.ActionRowInst.Panel);
            return;
        }

        if (!PlayerInRange(DungeonMasterInst, DungeonMasterInst.Player, SpiderInstance, 2))
        {
            SpiderInstance.ActionRowInst.Actions = CopyActionSet(approach, SpiderInstance.ActionRowInst.Panel);
            return;
        }

        if (HPmoreThan30Percent(SpiderInstance.MaxHP, SpiderInstance.CurrentHP, 30))
        {
            
            SpiderInstance.ActionRowInst.Actions = CopyActionSet(sneakyLeap, SpiderInstance.ActionRowInst.Panel);
            return;
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        SpiderInstance.ActionRowInst.Actions = null;
        return;
    }

    public override void InitializeActionPrototypes()
    {
        approach = InitializeApproach();
        rotate = InitializeRotate();
        sneakyLeap = InitializeSneakyLeap();
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