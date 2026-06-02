using System;
using System.Collections.Generic;
using UnityEngine;

public class MechanicPatternPicker : BasePatternPicker
{
    private List<IAction> _approach;
    private List<IAction> _rotate;
    private List<IAction> _chase;
    private List<IAction> _heavyPunch;
    private List<IAction> _chasingPunch;
    private List<IAction> _tantrums;
    private List<IAction> _retreat;

    private int _playerWasInCarriageLeft = 0;
    
    public override void FillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        IConditionCommand playerOnCellsAhead = new ActorIsOnCellsAheadCondition(_turnProcessor, 
        _levelMaster, 
        _levelMaster.Player, 
        _actor);

        IConditionCommand playerKeepsDistance = new ActorInRangeOfXCellsFromOtherActorCondition(_turnProcessor, 
        _levelMaster, 
        _levelMaster.Player, 
        _actor, 
        2);

        IConditionCommand HPMoreThan30Percent = new CurrentHPIsMoreThanXPercentCondition(_turnProcessor, 
        _levelMaster, 
        _actor.MaxHP,
        _actor.CurrentHP,
        30);

        IConditionCommand playerInTheCarriageLeft = new ActorInRangeOfCellsCondition(_turnProcessor, _levelMaster, _levelMaster.Player, 0, 3);

        IConditionCommand mechanicInTheCarriageLeft = new ActorInRangeOfCellsCondition(_turnProcessor, _levelMaster, _actor, 0, 3);

        // Func<float, float, float, bool> HPmoreThan30Percent = HPBasedCondition.CurrentHPIsMoreThanXPercent;
        // Func<TurnProcessor, IActor, IActor, int, bool> PlayerKeepsDistance = CellBasedCondition.ActorInRangeOfXCellsFromOtherActor;
        // Func<TurnProcessor, IActor, IActor, bool> PlayerAhead = CellBasedCondition.ActorIsOnCellsAhead;

        // Func<TurnProcessor, IActor, int, int, bool> ActorInTheCarriageLeft = CellBasedCondition.ActorInRangeOfCells;

        // if (!PlayerAhead(TurnProcessorInst, LevelMasterInst.Player, MechanicInstance)){}
        if (!playerOnCellsAhead.Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_rotate, _actor.ActionRowInst.Panel);
            return;
        }

        // if (!PlayerKeepsDistance(TurnProcessorInst, LevelMasterInst.Player, MechanicInstance, 2)){}
        if (!playerKeepsDistance.Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_approach, _actor.ActionRowInst.Panel);
            return;
        }

        if (
        playerInTheCarriageLeft.Execute() && mechanicInTheCarriageLeft.Execute() && _playerWasInCarriageLeft < 2
        )
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_retreat, _actor.ActionRowInst.Panel);
            _playerWasInCarriageLeft += 1;
            return;
        }

        if (HPMoreThan30Percent.Execute())
        {
            int randomInt = UnityEngine.Random.Range(1, 4);
            if (randomInt == 1)
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_chase, _actor.ActionRowInst.Panel);
                return;
            }
            else if (randomInt == 2)
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_heavyPunch, _actor.ActionRowInst.Panel);
                return;
            }
            else if (randomInt == 3)
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_chasingPunch, _actor.ActionRowInst.Panel);
                return;
            }
        }
        else
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_tantrums, _actor.ActionRowInst.Panel);
            return;
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        _actor.ActionRowInst.Actions = null;
        return;
    }

    public override void InitializeActionPrototypes()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _chase = InitializeChase();
        _heavyPunch = InitializeHeavyPunch();
        _chasingPunch = InitializeChasingPunch();
        _tantrums = InitializeTantrum();
        _retreat = InitializeRetreat();
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

    private List<IAction> InitializeChase()
    {
        List<IAction> actions = new();

        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(heavyWalk);

        return actions;
    }

    private List<IAction> InitializeHeavyPunch()
    {
        List<IAction> actions = new();

        IAction roar = new AngryRoar();
        roar.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(roar);

        IAction strike = new Strike();
        strike.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(strike);

        return actions;
    }

    private List<IAction> InitializeChasingPunch()
    {
        List<IAction> actions = new();
        
        IAction heavyWalk = new HeavyWalk();
        heavyWalk.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(heavyWalk);

        IAction heavyWalk1 = new HeavyWalk();
        heavyWalk1.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(heavyWalk1);

        IAction strike = new Strike();
        strike.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(strike);

        return actions;
    }

        
    private List<IAction> InitializeTantrum()
    {
        List<IAction> actions = new();

        IAction tantrum = new Tantrum();
        tantrum.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(tantrum);

        return actions;
    }

    private List<IAction> InitializeRetreat()
    {
        List<IAction> actions = new();

        IAction moveBack = new MoveOneTileBackwards();
        moveBack.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(moveBack);

        IAction moveBack1 = new MoveOneTileBackwards();
        moveBack1.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(moveBack1);

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