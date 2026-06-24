using System.Collections.Generic;
using UnityEngine;

public class SwamperSummonPatternPicker : BasePatternPicker
{
    private List<BaseAction> _strike;
    private List<BaseAction> _sacrifice;
    private int _turnsTillSacrifice;

    public override void ChildFillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        // if (_turnsTillSacrifice > 0)
        // {
        //     _actor.ActionRowInst.Actions = CopyActionSet(_strike, _actor.ActionRowInst.Panel);
        //     _turnsTillSacrifice -= 1;
        //     return;
        // }
        // else
        // {
            _actor.ActionRowInst.Actions = CopyActionSet(_sacrifice, _actor.ActionRowInst.Panel);
            return;
        // }
    }

    public override void InitializeChild()
    {
        _turnsTillSacrifice = 2;
        _strike = InitializeStrike();
        _sacrifice = InitializeSacrifice();
    }

    private List<BaseAction> InitializeStrike()
    {
        List<BaseAction> actions = new();

        BaseAction strike = new Strike();
        strike.Initialize(_actor, _turnProcessor, _levelMaster, 0, "AttackActionUI/Strike");
        actions.Add(strike);

        return actions;
    }

    private List<BaseAction> InitializeSacrifice()
    {
        List<BaseAction> actions = new();

        BaseAction healSummoner = new HealSummoner((_actor as Summon).SummonerInst);
        healSummoner.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/HealSummoner");
        actions.Add(healSummoner);

        BaseAction die = new Die();
        die.Initialize(_actor, _turnProcessor, _levelMaster, 0, "SkillActionUI/Die/Death");
        actions.Add(die);

        return actions;
    }
}