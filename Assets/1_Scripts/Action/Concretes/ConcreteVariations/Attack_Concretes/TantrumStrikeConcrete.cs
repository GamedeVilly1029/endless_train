using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantrumStrikeConcrete : ValueConcrete
{
    public TantrumStrikeConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    int value
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
    }

    public override IEnumerator ChildExecute()
    {
        for (int i = TurnProcessorInst.CurrentActor.FightBasedActionHistory.Count - 1; i >= 0; i--)
        {
            if (TurnProcessorInst.CurrentActor.FightBasedActionHistory[i] is Tantrum)
            {
                yield return new StrikeConcrete(
                    TurnProcessorInst,
                    LevelMasterInst,
                    ActionOfThisConcrete,
                    null,
                    ActionConcreteTag.Attack,
                    Value
                ).Execute();
            }
            else
            {
                break;
            }
        }

        yield return new StrikeConcrete(
            TurnProcessorInst,
            LevelMasterInst,
            ActionOfThisConcrete,
            null,
            ActionConcreteTag.Attack,
            Value
        ).Execute();
    }
}