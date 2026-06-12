using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantrumStrikeConcrete : ValueConcrete
{
    BaseActor _striker;
    public TantrumStrikeConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    BaseAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    int value,
    BaseActor striker
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _striker = striker;
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
                    Value,
                    _striker
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
            Value,
            _striker
        ).Execute();
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new TantrumStrikeConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _striker);
    }
}