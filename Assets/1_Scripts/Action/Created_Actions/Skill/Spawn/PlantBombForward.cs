using UnityEngine;

public class PlantBombForward: BaseAction
{
    private EnemyInstantiationInfo _info;

    public PlantBombForward(EnemyInstantiationInfo info)
    {
        _info = info;
    }
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new SpawnForwardConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, _info, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        PlantBombForward actionClone = new(_info)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}