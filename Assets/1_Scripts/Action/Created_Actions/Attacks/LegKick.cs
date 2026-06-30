using System.Collections.Generic;
using UnityEngine;

public class LegKick : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new();
        List<IConditionCommand> conds = new()
        {
            new ActionRowHasOnly1Action(TurnProcessorInst, LevelMasterInst, Actor)
        };
        BaseConcrete strongStrike = new StrikeConcrete(TurnProcessorInst, LevelMasterInst, this, conds, ActionConcreteTag.Attack, 10, Actor);
        ActionConstruct.Add(strongStrike);

        conds = new()
        {
            new ConcreteHistoryIsEmptyCondition(TurnProcessorInst, LevelMasterInst, this)
        };
        BaseConcrete strike = new StrikeConcrete(TurnProcessorInst, LevelMasterInst, this, conds, ActionConcreteTag.Attack, 5, Actor);
        ActionConstruct.Add(strike);
    }

    public override BaseAction CreateClone(Transform transform)
    {
        LegKick actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}
