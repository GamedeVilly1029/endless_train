using UnityEngine;

public class BasicDefend: BaseAction 
{
    

    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new DefenseConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        BasicDefend actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}