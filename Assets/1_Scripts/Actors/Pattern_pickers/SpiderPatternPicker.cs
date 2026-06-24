using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatternPicker : BasePatternPicker
{
    private List<BaseAction> _approach;
    private List<BaseAction> _rotate;
    private List<BaseAction> _sneakyLeap;
    private List<BaseAction> _stunningShout;

    public override void ChildFillActionRowOrBelt()
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

    public override void InitializeChild()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _sneakyLeap = InitializeSneakyLeap();
        _stunningShout = InitializeStunningShout();
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

    private List<BaseAction> InitializeSneakyLeap()
    {
        List<BaseAction> actions = new();
        BaseAction dash = new Dash();
        dash.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/Dash");
        actions.Add(dash);

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster,0, "AttackActionUI/Strike");
        actions.Add(strike);

        BaseAction moveBack = new MoveOneTileBackwards();
        moveBack.Initialize(_actor, _turnProcessor, _levelMaster,0, "MovementActionUI/WalkBackwards");
        actions.Add(moveBack);

        return actions;
    }

    private List<BaseAction> InitializeStunningShout()
    {
        List<BaseAction> actions = new();

        BaseAction stunFirstNextTurn = new StunFirstPlayerActionNextTurn();
        stunFirstNextTurn.Initialize(_actor, _turnProcessor, _levelMaster,0, "SkillActionUI/Stun");
        actions.Add(stunFirstNextTurn);

        return actions;
    }
}