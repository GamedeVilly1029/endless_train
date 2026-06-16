using System;
using System.Collections.Generic;
using UnityEngine;

public class SwamperPatternPicker : BasePatternPicker
{
    private List<BaseAction> _spawnLeft;
    private List<BaseAction> _spawnRight;
    private List<BaseAction> _spawnBoth;
    private List<BaseAction> _empower;

    public override void FillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        if 
        (
            new CellAtIdxIsEmpty(_turnProcessor, _levelMaster, _actor.PositionCellIndex - 1).Execute() && 
            new CellAtIdxIsEmpty(_turnProcessor, _levelMaster, _actor.PositionCellIndex + 1).Execute()
        )
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_spawnBoth, _actor.ActionRowInst.Panel);
            return;
        }

        if (new CellAtIdxIsEmpty(_turnProcessor, _levelMaster, _actor.PositionCellIndex - 1).Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_spawnLeft, _actor.ActionRowInst.Panel);
            return;
        }

        if (new CellAtIdxIsEmpty(_turnProcessor, _levelMaster, _actor.PositionCellIndex + 1).Execute())
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_spawnRight, _actor.ActionRowInst.Panel);
            return;
        }


        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        _actor.ActionRowInst.Actions.Clear();
        return;
    }

    public override void InitializeChild()
    {
        _spawnLeft = InitializeSpawnLeft();
        _spawnRight = InitializeSpawnRight();
        _spawnBoth = InitializeSpawnBoth();
        _empower = InitializeEmpower();
    }

    private List<BaseAction> InitializeSpawnLeft()
    {
        List<BaseAction> actions = new();

        BaseAction spawnLeft = new SummonLeft(_actor as Summoner, Resources.Load<EnemyInstantiationInfo>("Summon/Swamper/Left"));
        spawnLeft.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Summon/SummonLeft");
        actions.Add(spawnLeft);

        return actions;
    }

    private List<BaseAction> InitializeSpawnRight()
    {
        List<BaseAction> actions = new();
        
        BaseAction spawnRight = new SummonRight(_actor as Summoner, Resources.Load<EnemyInstantiationInfo>("Summon/Swamper/Right"));
        spawnRight.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Summon/SummonRight");
        actions.Add(spawnRight);

        return actions;
    }

    private List<BaseAction> InitializeSpawnBoth()
    {
        List<BaseAction> actions = new();

        BaseAction spawnLeft = new SummonLeft(_actor as Summoner, Resources.Load<EnemyInstantiationInfo>("Summon/Swamper/Left"));
        spawnLeft.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Summon/SummonLeft");
        actions.Add(spawnLeft);
        
        BaseAction spawnRight = new SummonRight(_actor as Summoner, Resources.Load<EnemyInstantiationInfo>("Summon/Swamper/Right"));
        spawnRight.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Summon/SummonRight");

        actions.Add(spawnRight);

        return actions;
    }

    private List<BaseAction> InitializeEmpower()
    {
        List<BaseAction> actions = new();

        BaseAction empowerAll = new AllSummonDMGIncrease(_actor as Summoner);
        empowerAll.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Summon/SummonerSkill/SummonNextAttackDMGIncrease");

        return actions;
    }
}