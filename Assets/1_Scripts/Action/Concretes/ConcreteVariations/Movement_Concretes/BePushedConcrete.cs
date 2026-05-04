using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BePushedConcrete : BaseConcrete
{
    private IActor _actorToPush;
    private bool _pushRight;

    public BePushedConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    IActor actorToPush,
    bool pushRight
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actorToPush = actorToPush;
        _pushRight = pushRight;
    }

    public override IEnumerator ChildExecute()
    {
        LevelMasterInst.Cells[_actorToPush.PositionCellIndex].EnityOccupyingThisCell = null;
        Vector2 start = LevelMasterInst.Cells[_actorToPush.PositionCellIndex].CellPosition;
        Vector2 end = MovementLowLevelConcrete.BePushedCalculator(_actorToPush, _pushRight);

        ParticlePlayer.StartBePushed(_actorToPush);

        yield return MovementLowLevelConcrete.StepFlat(_actorToPush, start, end, 0.2f);
        _actorToPush.TransformReference.position = end;

        ParticlePlayer.StopBePushed(_actorToPush);
    }
}