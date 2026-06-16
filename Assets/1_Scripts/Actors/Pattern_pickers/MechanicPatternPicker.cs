using System;
using System.Collections.Generic;
using UnityEngine;

public class MechanicPatternPicker : BasePatternPicker
{
    private List<BaseAction> _approach;
    private List<BaseAction> _rotate;
    private List<BaseAction> _chase;
    private List<BaseAction> _heavyPunch;
    private List<BaseAction> _chasingPunch;
    private List<BaseAction> _tantrums;
    private List<BaseAction> _retreat;

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

    public override void InitializeChild()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _chase = InitializeChase();
        _heavyPunch = InitializeHeavyPunch();
        _chasingPunch = InitializeChasingPunch();
        _tantrums = InitializeTantrum();
        _retreat = InitializeRetreat();
    }

    private List<BaseAction> InitializeApproach()
    {
        List<BaseAction> actions = new();

        BaseAction move = new MoveOneTileForward();
        move.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkForward");
        actions.Add(move);

        BaseAction move1 = new MoveOneTileForward();
        move1.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkForward");
        actions.Add(move1);

        return actions;
    }


    private List<BaseAction> InitializeRotate()
    {
        List<BaseAction> actions = new();

        BaseAction rotate = new Rotate();
        rotate.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/Rotate");
        actions.Add(rotate);

        return actions;
    } 

    private List<BaseAction> InitializeChase()
    {
        List<BaseAction> actions = new();

        BaseAction heavyWalk = new HeavyWalk();
        heavyWalk.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/HeavyWalk");
        actions.Add(heavyWalk);

        return actions;
    }

    private List<BaseAction> InitializeHeavyPunch()
    {
        List<BaseAction> actions = new();

        BaseAction roar = new AngryRoar();
        roar.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/AngryRoar");
        actions.Add(roar);

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        return actions;
    }

    private List<BaseAction> InitializeChasingPunch()
    {
        List<BaseAction> actions = new();
        
        BaseAction heavyWalk = new HeavyWalk();
        heavyWalk.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/HeavyWalk");
        actions.Add(heavyWalk);

        BaseAction heavyWalk1 = new HeavyWalk();
        heavyWalk1.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/HeavyWalk");
        actions.Add(heavyWalk1);

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        return actions;
    }

    private List<BaseAction> InitializeTantrum()
    {
        List<BaseAction> actions = new();

        BaseAction tantrum = new Tantrum();
        tantrum.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Tantrum");
        actions.Add(tantrum);

        return actions;
    }

    private List<BaseAction> InitializeRetreat()
    {
        List<BaseAction> actions = new();

        BaseAction moveBack = new MoveOneTileBackwards();
        moveBack.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(moveBack);

        BaseAction moveBack1 = new MoveOneTileBackwards();
        moveBack1.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(moveBack1);

        return actions;
    }
}