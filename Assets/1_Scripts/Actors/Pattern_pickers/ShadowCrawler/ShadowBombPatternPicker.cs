using UnityEngine;
using System.Collections.Generic;

public class ShadowBombPatternPicker : BasePatternPicker
{
    private List<BaseAction> _wither;
    private List<BaseAction> _stall;
    private List<BaseAction> _explode;

    private int _turnsTillSacrifice;

    public override void ChildFillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        if (_turnsTillSacrifice > 0)
        {
            if (new PlayerIsInAdjacentCells(_turnProcessor, _levelMaster, _actor).Execute())
            {
                _actor.ActionRowInst.Actions = CopyActionSet(_explode, _actor.ActionRowInst.Panel);
                _turnsTillSacrifice -= 1;
                return;
            }
            else
            {

                _actor.ActionRowInst.Actions = CopyActionSet(_stall, _actor.ActionRowInst.Panel);
                _turnsTillSacrifice -= 1;
                return;
            }
        }
        else
        {
            _actor.ActionRowInst.Actions = CopyActionSet(_wither, _actor.ActionRowInst.Panel);
            return;
        }
    }

    public override void InitializeChild()
    {
        _turnsTillSacrifice = 2;
        _wither = InitializeWither();
        _stall = InitializeStall();
        _explode = InitializeExplode();
    }

    private List<BaseAction> InitializeStall()
    {
        List<BaseAction> actions = new();

        BaseAction stall = new Stall();
        stall.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Wait/Stall");
        actions.Add(stall);

        return actions;
    }

    private List<BaseAction> InitializeWither()
    {
        List<BaseAction> actions = new();

        BaseAction die = new Die();
        die.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Die/Death");
        actions.Add(die);

        return actions;
    }

    private List<BaseAction> InitializeExplode()
    {
        List<BaseAction> actions = new();

        BaseAction stall = new Stall();
        stall.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Wait/Stall");
        actions.Add(stall);

        BaseAction explode = new Explode();
        explode.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Die/Explode");
        actions.Add(explode);

        return actions;
    }
}