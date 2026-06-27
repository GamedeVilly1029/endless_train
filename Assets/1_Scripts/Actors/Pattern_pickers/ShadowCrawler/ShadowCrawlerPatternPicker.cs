using System.Collections.Generic;
using UnityEngine;

public class ShadowCrawlerPatternPicker : BasePatternPicker
{
    private List<BaseAction> _approach;
    private List<BaseAction> _rotate;
    private List<BaseAction> _plantBomb;
    private List<BaseAction> _stepForward;
    private List<BaseAction> _stepBackwards;
    private List<BaseAction> _sneakBehind;
    private List<BaseAction> _blockade;
    private List<BaseAction> _strike;

    private List<BaseAction> _previousPattern;

    public override void ChildFillActionRowOrBelt()
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

        if (!playerOnCellsAhead.Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_rotate, _actor.ActionRowInst.Panel);
            return;
        }

        if (!playerKeepsDistance.Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_approach, _actor.ActionRowInst.Panel);
            return;
        }




        if (_previousPattern == _sneakBehind)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_blockade, _actor.ActionRowInst.Panel);
            return;
        }

        if
        (
            new DistanceToActorAheadIsLessThanX(_turnProcessor, _levelMaster, 1, _actor).Execute() 
            // new DistanceToActorAheadIsMoreThanX(_turnProcessor, _levelMaster, 0, _actor).Execute()
        )
        {
            if (new CellAfterFirstActorAheadIsEmpty(_turnProcessor, _levelMaster, _actor).Execute())
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_sneakBehind, _actor.ActionRowInst.Panel);
                return;
            }
            else
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_strike, _actor.ActionRowInst.Panel);
                return;
            }
        }

        int rng = Random.Range(0, 2);
        if (rng == 0)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_stepForward, _actor.ActionRowInst.Panel);
            return;
        }
        else if (rng == 1)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_stepBackwards, _actor.ActionRowInst.Panel);
            return;
        }

        // _actor.ActionRowInst.Actions = CopyActionSet(_stepBackwards, _actor.ActionRowInst.Panel);
        // return;
    }

    public override void InitializeChild()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _plantBomb = InitializePlantBomb();
        _stepForward = InitializeStepForward();
        _stepBackwards = InitializeStepBackwards();
        _sneakBehind = InitializeSneakBehind();
        _blockade = InitializeBlockade();
        _strike = InitializeStrike();
    }


    private List<BaseAction> InitializeStrike()
    {
        List<BaseAction> actions = new();

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        return actions;
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

    private List<BaseAction> InitializePlantBomb()
    {
        List<BaseAction> actions = new();

        BaseAction retreat0 = new MoveOneTileBackwards();
        retreat0.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(retreat0);

        BaseAction bomb = new PlantBombForward(Resources.Load<EnemyInstantiationInfo>("Spawn/ShadowBomb"));
        bomb.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Spawn/Bomb");
        actions.Add(bomb);

        BaseAction retreat = new MoveOneTileBackwards();
        retreat.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(retreat);

        return actions;
    }

    private List<BaseAction> InitializeStepForward()
    {
        List<BaseAction> actions = new();

        BaseAction move = new MoveOneTileForward();
        move.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkForward");
        actions.Add(move);
        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        return actions;
    }

    private List<BaseAction> InitializeStepBackwards()
    {
        List<BaseAction> actions = new();

        BaseAction move = new MoveOneTileBackwards();
        move.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkBackwards");
        actions.Add(move);

        return actions;
    }

    private List<BaseAction> InitializeSneakBehind()
    {
        List<BaseAction> actions = new();

        BaseAction plantBehind = new PlantBombBehind(Resources.Load<EnemyInstantiationInfo>("Spawn/ShadowBomb"));
        plantBehind.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Spawn/Bomb");
        actions.Add(plantBehind);

        BaseAction sneak = new TeleportBehindActorAhead();
        sneak.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/TeleportBehind");
        actions.Add(sneak);

        BaseAction push = new Push();
        push.Initialize(_actor, _turnProcessor, _levelMaster, 0, "PushActionUI/Push");
        actions.Add(push);

        return actions;
    }

    private List<BaseAction> InitializeBlockade()
    {
        List<BaseAction> actions = new();

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        BaseAction strike1 = new Strike();
        strike1.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike1);

        BaseAction strike2 = new Strike();
        strike2.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike2);

        return actions;
    }

    public override List<BaseAction> CopyActionSet(List<BaseAction> set, RectTransform UIPanel)
    {
        _previousPattern = set;
        return base.CopyActionSet(set, UIPanel);
    }
}