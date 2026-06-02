using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseConcrete : ValueConcrete
{
    private BaseActor _actorToDefend;
    public DefenseConcrete(
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        IAction actionOfThisConcrete, 
        List<IConditionCommand> extraConditions, 
        ActionConcreteTag tag, 
        int value,
        BaseActor actorToDefend) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _actorToDefend = actorToDefend;
    }

    public override IEnumerator ChildExecute()
    {
        ActorParticlePlayer.PlayParticles(_actorToDefend, ParticleType.BasicDefend);
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("basicDefend");
        yield return _actorToDefend.Defense += Value;
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new DefenseConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, clonedAction.Actor);
    }
}