using System.Collections.Generic;
using UnityEngine;

public class FattyPatternPicker : BasePatternPicker
{
    private List<BaseAction> _approach;
    private List<BaseAction> _rotate;
    private List<BaseAction> _chase;
    private List<BaseAction> _shockWave;
    private List<BaseAction> _heal;

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

        int randomInt = UnityEngine.Random.Range(1, 4);
        if (randomInt == 1)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_chase, _actor.ActionRowInst.Panel);
            return;
        }
        else if (randomInt == 2)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_shockWave, _actor.ActionRowInst.Panel);
            return;
        }
        else if (randomInt == 3)
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_heal, _actor.ActionRowInst.Panel);
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
        _shockWave = InitializeWave();
        _heal = InitializeHeal();
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

        BaseAction stepForward = new MoveOneTileForward();
        stepForward.Initialize(_actor, _turnProcessor, _levelMaster, 0, "MovementActionUI/WalkForward");
        actions.Add(stepForward);

        BaseAction block = new BasicDefend();
        block.Initialize(_actor, _turnProcessor, _levelMaster, 0, "DefenseActionUI/BasicDefend");
        actions.Add(block);

        BaseAction block1 = new BasicDefend();
        block1.Initialize(_actor, _turnProcessor, _levelMaster, 0, "DefenseActionUI/BasicDefend");
        actions.Add(block1);

        BaseAction block2 = new BasicDefend();
        block2.Initialize(_actor, _turnProcessor, _levelMaster, 0, "DefenseActionUI/BasicDefend");
        actions.Add(block2);

        return actions;
    }

    private List<BaseAction> InitializeWave()
    {
        List<BaseAction> actions = new();

        BaseAction wave = new ShockWave();
        wave.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/ShockWave");
        actions.Add(wave);

        return actions;
    }

    private List<BaseAction> InitializeHeal()
    {
        List<BaseAction> actions = new();

        BaseAction heal = new Heal();
        heal.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Heal");
        actions.Add(heal);

        return actions;
    }

    
}