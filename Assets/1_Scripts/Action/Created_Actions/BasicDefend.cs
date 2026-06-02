using UnityEngine;

public class BasicDefend: BaseAction 
{
    public override void InitializeChildAction()
    {
        CooldownMax = 0;
        Cooldown = 0;
        if (Resources.Load<GameObject>("DefenseActionUI/BasicDefendActionUI") != null)
        {
            UIRepresentation = Resources.Load<GameObject>("DefenseActionUI/BasicDefendActionUI");
        }
        else
        {
            Debug.LogError("Resources.Load can't find UIRepresentationAsset");
        }
        InitializeConstruct();
    }

    private void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new DefenseConcrete(TurnProcessorInstance, LevelMasterInstance, this, null, ActionConcreteTag.Attack, 5, Actor)
        };
    }

    public override IAction CreateClone(Transform transform)
    {
        BasicDefend actionClone = new()
        {
            TurnProcessorInstance = TurnProcessorInstance,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}