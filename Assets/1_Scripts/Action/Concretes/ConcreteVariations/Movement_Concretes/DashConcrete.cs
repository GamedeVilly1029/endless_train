using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashConcrete : ValueConcrete
{
    private IActor _caller;

    public DashConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    int value,
    IActor caller
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _caller = caller;
    }

    public override IEnumerator ChildExecute()
    {
        int chargeRange = Value;

        if (!new CellAheadExistsCondition(TurnProcessorInst, LevelMasterInst, _caller).Execute() ||
            !new CellAheadIsEmptyCondition(TurnProcessorInst, LevelMasterInst, _caller).Execute())
        {
            yield break;
        }

        if (_caller.IsFacingRight())
        {
            int maxCheckPos = _caller.PositionCellIndex + chargeRange;
            for (int i = _caller.PositionCellIndex + 2; i <= maxCheckPos; i++)
            {
                if (i < LevelMasterInst.Cells.Count)
                {
                    if (LevelMasterInst.Cells[i].EnityOccupyingThisCell != null)
                    {
                        yield return new SlideConcrete(
                            TurnProcessorInst,
                            LevelMasterInst,
                            ActionOfThisConcrete,
                            null,
                            ActionConcreteTag.Move,
                            _caller,
                            i - 1
                        );
                        yield break;
                    }
                }
            }
        }
        else
        {
            int maxCheckPos = _caller.PositionCellIndex - chargeRange;
            for (int i = _caller.PositionCellIndex - 2; i >= maxCheckPos; i--)
            {
                if (i > 0)
                {
                    if (LevelMasterInst.Cells[i].EnityOccupyingThisCell != null)
                    {
                        yield return new SlideConcrete(
                            TurnProcessorInst,
                            LevelMasterInst,
                            ActionOfThisConcrete,
                            null,
                            ActionConcreteTag.Move,
                            _caller,
                            i + 1
                        );
                        yield break;
                    }
                }
            }
        }

        yield return MovementLowLevelConcrete.StepForwardOrBackwards(_caller, Resources.Load<MoveData>("StepData"), _caller.IsFacingRight());
        yield return GlobalLowLevelConcrete.Pause;
    }
}