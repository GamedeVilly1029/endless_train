using System;
using System.Collections.Generic;
using UnityEngine;

public class MechanicPatternPicker : MonoBehaviour, IPatternPicker
{
    public Mechanic Mechanic;
    public DungeonMaster DungeonMasterInst;

    public List<IAction> approach;
    public List<IAction> rotate;
    public List<IAction> chase;
    public List<IAction> heavyPunch;
    public List<IAction> chasingPunch;
    public List<IAction> tantrums;
    
    public List<IAction> ReturnPattern()
    {
        Func<int, int, int, bool> HPmoreThan30Percent = HPBasedCondition.CurrentHPIsMoreThanXPercent;
        Func<DungeonMaster, IActor, IActor, int, bool> PlayerInRange = CellBasedCondition.ActorInRangeOfXCells;
        Func<DungeonMaster, IActor, IActor, bool> PlayerAhead = CellBasedCondition.ActorIsOnCellsAhead;

        if (!PlayerInRange(DungeonMasterInst, DungeonMasterInst.Player, Mechanic, 2))
        {
            if (PlayerAhead(DungeonMasterInst, DungeonMasterInst.Player, Mechanic))
            {
                return CopyActionSet(approach, DungeonMasterInst.Mechanic.ActionRowPanel);
            }
            else
            {
                return CopyActionSet(rotate, DungeonMasterInst.Mechanic.ActionRowPanel);
            }
        }

        if (HPmoreThan30Percent(Mechanic.MaxHP, Mechanic.CurrentHP, 30))
        {
            int randomInt = UnityEngine.Random.Range(1, 4);
            if (randomInt == 1)
            {
                return CopyActionSet(chase, DungeonMasterInst.Mechanic.ActionRowPanel);
            }
            else if (randomInt == 2)
            {
                return CopyActionSet(heavyPunch, DungeonMasterInst.Mechanic.ActionRowPanel);
            }
            else if (randomInt == 3)
            {
                return CopyActionSet(chasingPunch, DungeonMasterInst.Mechanic.ActionRowPanel);
            }
        }
        else
        {
            return CopyActionSet(tantrums, DungeonMasterInst.Mechanic.ActionRowPanel);
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        return null;
    }

    

    public void InitializeActionPatterns()
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
        move.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(move);

        IAction move1 = new MoveOneTileForward();
        move1.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(move1);

        return actions;
    }

    

    private List<IAction> InitializeRotate()
    {
        List<IAction> actions = new();

        IAction rotate = new Rotate();
        rotate.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(rotate);

        return actions;
    }

    private List<IAction> InitializeChase()
    {
        List<IAction> actions = new();

        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(heavyWalk);

        return actions;
    }

    private List<IAction> InitializeHeavyPunch()
    {
        List<IAction> actions = new();

        IAction roar = new AngryRoar();
        roar.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(roar);

        IAction strike = new Strike();
        strike.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(strike);

        return actions;
    }

    private List<IAction> InitializeChasingPunch()
    {
        List<IAction> actions = new();
        
        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(heavyWalk);

        IAction strike = new Strike();
        strike.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(strike);

        return actions;
    }

        
    private List<IAction> InitializeTantrum()
    {
        List<IAction> actions = new();

        IAction tantrum = new Tantrum();
        tantrum.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(tantrum);

        IAction tantrum1 = new Tantrum();
        tantrum1.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(tantrum1);

        IAction tantrum2 = new Tantrum();
        tantrum2.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(tantrum2);

        IAction tantrum3 = new Tantrum();
        tantrum3.InitializeAction(Mechanic, DungeonMasterInst);
        actions.Add(tantrum3);

        return actions;
    }

    private List<IAction> CopyActionSet(List<IAction> set, RectTransform UIPanel)
    {
        List<IAction> copies = new();
        foreach (IAction action in set)
        {
           IAction copy = action.CloneAndInstantiateUI(UIPanel);
           copies.Add(copy);
        }
        return copies;
    }
}
