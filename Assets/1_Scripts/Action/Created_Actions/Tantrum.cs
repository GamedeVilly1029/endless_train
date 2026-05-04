using UnityEngine;
using System.Collections.Generic;
using System;

public class Tantrum : BaseAction
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("TantrumActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("TantrumActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        List<IConditionCommand> conditions = new()
        {
            new LastActionIsNotThisActionCondition(TurnProcessorInstance, LevelMasterInstance),
        };

        ActionConstruct = new();
        RemoveTantrumVulnerabilityConcrete removeVuln = new(TurnProcessorInstance, LevelMasterInstance, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(removeVuln);

        StrikeConcrete strike = new(TurnProcessorInstance, LevelMasterInstance, this, conditions, ActionConcreteTag.Attack, 5);
        ActionConstruct.Add(strike);

        AddTantrumVulnerabilityConcrete addVuln = new(TurnProcessorInstance, LevelMasterInstance, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(addVuln);

        conditions = new()
        {
            new ConcreteHistoryIsEmptyCondition(TurnProcessorInstance, LevelMasterInstance)
        };

        TantrumStrikeConcrete tanStrike = new (TurnProcessorInstance, LevelMasterInstance, this, conditions, ActionConcreteTag.Attack, 5);
        ActionConstruct.Add(tanStrike);

        AddTantrumVulnerabilityConcrete addVuln1 = new(TurnProcessorInstance, LevelMasterInstance, this, conditions, ActionConcreteTag.Skill);
        ActionConstruct.Add(addVuln1);
    }

    public override IAction CreateClone(Transform transform)
    {
        Tantrum actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = UnityEngine.Object.Instantiate(UIRepresentation, transform),
        };

        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}