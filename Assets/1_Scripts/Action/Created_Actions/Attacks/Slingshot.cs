using UnityEngine;

public class Slingshot : BaseAction 
{
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new SlingshotConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Attack, 5, Actor, 0.5f, 1f, 2f)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        Slingshot actionClone = new()
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}