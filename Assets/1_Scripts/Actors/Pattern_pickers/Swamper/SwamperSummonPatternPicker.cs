using System.Collections.Generic;
using UnityEngine;

public class SwamperSummonPatternPicker: BasePatternPicker
{
    private List<IAction> _strike;
    private List<IAction> _sacrifice;

    private int _turnsTillSacrifice = 2;

    public override void FillActionRowOrBelt()
    {
        _actor.ActionRowInst.Actions.Clear();

        if (_turnsTillSacrifice > 0)
        {
            _actor.ActionRowInst.Actions = _strike;
        }
        else
        {
            _actor.ActionRowInst.Actions = _sacrifice;
        }

        Debug.LogError("Bad enemy AI - none of the predefined actions was selected");
        _actor.ActionRowInst.Actions = null;
        return;
    }

    public override void InitializeActionPrototypes()
    {

    }

    private List<IAction> InitializeStrike()
    {
        List<IAction> actions = new();

        IAction strike = new Strike();
        strike.InitializeAction(_actor, _turnProcessor, _levelMaster);
        actions.Add(strike);

        return actions;
    }

    private List<IAction> InitializeSacrifice()
    {
        List<IAction> actions = new();

        IAction healSummoner = new HealSummoner();
        // healSummoner.InitializeAction()

        return actions;
    }

    private List<IAction> CopyActionSet(List<IAction> set, RectTransform UIPanel)
    {
        List<IAction> copies = new();
        foreach (IAction action in set)
        {
            IAction copy = action.CloneAndInstantiateUI(UIPanel, action);
            copies.Add(copy); }
        return copies;
    }
}
