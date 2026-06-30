using UnityEngine;
using System.Collections.Generic;
using System;

public class Tantrum : BaseAction
{
    public override void InitializeConstruct()
    {
        List<IConditionCommand> conditions = new()
        {
            new LastActionIsNotThisActionCondition(TurnProcessorInst, LevelMasterInst),
        };

        ActionConstruct = new();
        RemoveTantrumVulnerabilityConcrete removeVuln = new(TurnProcessorInst, LevelMasterInst, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(removeVuln);

        StrikeConcrete strike = new(TurnProcessorInst, LevelMasterInst, this, conditions, ActionConcreteTag.Attack, 5, Actor);
        ActionConstruct.Add(strike);

        AddTantrumVulnerabilityConcrete addVuln = new(TurnProcessorInst, LevelMasterInst, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(addVuln);

        conditions = new()
        {
            new ConcreteHistoryIsEmptyCondition(TurnProcessorInst, LevelMasterInst, this)
        };

        TantrumStrikeConcrete tanStrike = new (TurnProcessorInst, LevelMasterInst, this, conditions, ActionConcreteTag.Attack, 5, Actor);
        ActionConstruct.Add(tanStrike);

        AddTantrumVulnerabilityConcrete addVuln1 = new(TurnProcessorInst, LevelMasterInst, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(addVuln1);
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Tantrum actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}