using System;
using System.Collections.Generic;
using UnityEngine;

public class MechanicPatternPicker : BasePatternPicker
{
    public Mechanic MechanicInstance;
    public DungeonMaster DungeonMasterInst;

    public List<IAction> approach;
    public List<IAction> rotate;
    public List<IAction> chase;
    public List<IAction> heavyPunch;
    public List<IAction> chasingPunch;
    public List<IAction> tantrums;
    
    public override void FillActionRowOrBelt()
    {
        MechanicInstance.ActionRowInst.Actions.Clear();

        Func<float, float, float, bool> HPmoreThan30Percent = HPBasedCondition.CurrentHPIsMoreThanXPercent;
        Func<DungeonMaster, IActor, IActor, int, bool> PlayerInRange = CellBasedCondition.ActorInRangeOfXCells;
        Func<DungeonMaster, IActor, IActor, bool> PlayerAhead = CellBasedCondition.ActorIsOnCellsAhead;

        if (!PlayerAhead(DungeonMasterInst, DungeonMasterInst.Player, MechanicInstance))
        {
            MechanicInstance.ActionRowInst.Actions = CopyActionSet(rotate, MechanicInstance.ActionRowInst.Panel);
            return;
        }

        if (!PlayerInRange(DungeonMasterInst, DungeonMasterInst.Player, MechanicInstance, 2))
        {
            MechanicInstance.ActionRowInst.Actions = CopyActionSet(approach, MechanicInstance.ActionRowInst.Panel);
            return;
        }

        if (HPmoreThan30Percent(MechanicInstance.MaxHP, MechanicInstance.CurrentHP, 30))
        {
            int randomInt = UnityEngine.Random.Range(1, 4);
            if (randomInt == 1)
            {
                MechanicInstance.ActionRowInst.Actions = CopyActionSet(chase, MechanicInstance.ActionRowInst.Panel);
                return;
            }
            else if (randomInt == 2)
            {
                MechanicInstance.ActionRowInst.Actions = CopyActionSet(heavyPunch, MechanicInstance.ActionRowInst.Panel);
                return;
            }
            else if (randomInt == 3)
            {
                MechanicInstance.ActionRowInst.Actions = CopyActionSet(chasingPunch, MechanicInstance.ActionRowInst.Panel);
                return;
            }
        }
        else
        {
            MechanicInstance.ActionRowInst.Actions = CopyActionSet(tantrums, MechanicInstance.ActionRowInst.Panel);
            return;
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        MechanicInstance.ActionRowInst.Actions = null;
        return;
    }

    public override void InitializeActionPrototypes()
    {
        approach = InitializeApproach();
        rotate = InitializeRotate();
        chase = InitializeChase();
        heavyPunch = InitializeHeavyPunch();
        chasingPunch = InitializeChasingPunch();
        tantrums = InitializeTantrum();
    }

    private List<IAction> InitializeApproach()
    {
        List<IAction> actions = new();

        IAction move = new MoveOneTileForward();
        move.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(move);

        IAction move1 = new MoveOneTileForward();
        move1.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(move1);

        return actions;
    }


    private List<IAction> InitializeRotate()
    {
        List<IAction> actions = new();

        IAction rotate = new Rotate();
        rotate.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(rotate);

        return actions;
    }

    private List<IAction> InitializeChase()
    {
        List<IAction> actions = new();

        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(heavyWalk);

        return actions;
    }

    private List<IAction> InitializeHeavyPunch()
    {
        List<IAction> actions = new();

        IAction roar = new AngryRoar();
        roar.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(roar);

        IAction strike = new Strike();
        strike.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(strike);

        return actions;
    }

    private List<IAction> InitializeChasingPunch()
    {
        List<IAction> actions = new();
        
        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(heavyWalk);

        IAction heavyWalk1 = new HeavyWalk();
        heavyWalk1.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(heavyWalk1);

        IAction strike = new Strike();
        strike.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(strike);

        return actions;
    }

        
    private List<IAction> InitializeTantrum()
    {
        List<IAction> actions = new();

        IAction tantrum = new Tantrum();
        tantrum.InitializeAction(MechanicInstance, DungeonMasterInst);
        actions.Add(tantrum);

        return actions;
    }

    private List<IAction> CopyActionSet(List<IAction> set, RectTransform UIPanel)
    {
        List<IAction> copies = new();
        foreach (IAction action in set)
        {
           IAction copy = action.CloneAndInstantiateUI(UIPanel, action);
           copies.Add(copy);
        //    MechanicInstance.ActionRowInst.OnActionAdd.Invoke();
        }
        return copies;
    }
}