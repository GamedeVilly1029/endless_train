using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideConcrete : BaseConcrete
{
    private IActor _actor;
    private int _destinationCellIDX;

    public SlideConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag,
    IActor actor,
    int destinationCellIDX
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actor = actor;
        _destinationCellIDX = destinationCellIDX;
    }

    public override IEnumerator ChildExecute()
    {
        Vector2 start = LevelMasterInst.Cells[_actor.PositionCellIndex].CellPosition;
        Vector2 end = LevelMasterInst.Cells[_destinationCellIDX].CellPosition;

        LevelMasterInst.Cells[_actor.PositionCellIndex].EnityOccupyingThisCell = null;
        LevelMasterInst.Cells[_destinationCellIDX].EnityOccupyingThisCell = _actor;
        _actor.PositionCellIndex = _destinationCellIDX;

        ParticlePlayer.StartSlide(_actor);
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("slide");
        yield return MovementLowLevelConcrete.StepFlat(_actor, start, end, 0.2f);
        _actor.TransformReference.position = end;
        ParticlePlayer.StopSlide(_actor);
    }

    public override IConcrete Clone(IAction clonedAction)
    {
        return new SlideConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _actor, _destinationCellIDX);
    }
}