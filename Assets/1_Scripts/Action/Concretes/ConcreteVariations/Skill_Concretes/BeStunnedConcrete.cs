using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeStunnedConcrete : BaseConcrete
{
    public BeStunnedConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        yield return GlobalLowLevelConcrete.Pause;
        Debug.Log("Stunned!");
        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new BeStunnedConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag);
    }
}