using System.Collections.Generic;
using UnityEngine;

public class FattyPatternPicker : BasePatternPicker
{
    private List<IAction> _approach;
    private List<IAction> _rotate;
    private List<IAction> _chase;
    private List<IAction> _shockWave;
    private List<IAction> _heal;

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

    public override void InitializeActionPrototypes()
    {
        _approach = InitializeApproach();
        _rotate = InitializeRotate();
        _chase = InitializeChase();
        _shockWave = InitializeWave();
        _heal = InitializeHeal();
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

        IAction stepForward = new MoveOneTileForward();
        stepForward.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(stepForward);

        IAction block = new BasicDefend();
        block.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(block);

        IAction block1 = new BasicDefend();
        block1.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(block1);

        IAction block2 = new BasicDefend();
        block2.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(block2);

        return actions;
    }

    private List<IAction> InitializeWave()
    {
        List<IAction> actions = new();

        IAction wave = new ShockWave();
        wave.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(wave);

        return actions;
    }

    private List<IAction> InitializeHeal()
    {
        List<IAction> actions = new();

        IAction heal = new Heal();
        heal.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(heal);

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