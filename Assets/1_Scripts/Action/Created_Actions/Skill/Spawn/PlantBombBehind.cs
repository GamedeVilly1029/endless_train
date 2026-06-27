using UnityEngine;

public class PlantBombBehind : BaseAction
{
    private EnemyInstantiationInfo _info;

    public PlantBombBehind(EnemyInstantiationInfo info)
    {
        _info = info;
    }
    public override void InitializeConstruct()
    {
        ActionConstruct = new()
        {
            new SpawnBehindConcrete(TurnProcessorInst, LevelMasterInst, this, null, ActionConcreteTag.Skill, _info, Actor)
        };
    }

    public override BaseAction CreateClone(Transform transform)
    {
        PlantBombBehind actionClone = new(_info)
        {
            TurnProcessorInst = TurnProcessorInst,
            Actor = Actor,
            UIRepresentation = Object.Instantiate(UIRepresentation, transform),
        };
        actionClone.ActionConstruct = CloneActionConstruct(actionClone);

        return actionClone;
    }
}