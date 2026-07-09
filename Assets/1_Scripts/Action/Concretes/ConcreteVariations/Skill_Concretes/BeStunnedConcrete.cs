using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeStunnedConcrete : BaseConcrete
{
    public BeStunnedConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    BaseAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        Object.FindAnyObjectByType<AudioMaster>().PlaySound("ticking");
        yield return new ConfusionEffect(ActionOfThisConcrete.Actor, 0.5f).Execute();
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new BeStunnedConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag);
    }
}