using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveConcrete : ValueConcrete 
{
    private BaseActor _caster;
    private WaitForSeconds _castPause = new(0.33f);

    public ShockWaveConcrete
    (
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        BaseAction actionOfThisConcrete,
        List<IConditionCommand> extraConditions,
        ActionConcreteTag tag,
        int value,
        BaseActor caster
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _caster = caster;
    }

    public override IEnumerator ChildExecute()
    {
        yield return MovementLowLevelConcrete.StepArc
        (
            _caster,
            Resources.Load<MoveData>("StepData"),
            LevelMasterInst.Cells[_caster.PositionCellIndex].CellPosition,
            LevelMasterInst.Cells[_caster.PositionCellIndex].CellPosition
        );
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("shockWaveLarge");
        for (int i = 1; i < 4; i++)
        {
            if (new CellXDistanceFromCasterToLeftExists(TurnProcessorInst, LevelMasterInst, i, _caster.PositionCellIndex).Execute())
            {
                Cell existingCell = LevelMasterInst.Cells[_caster.PositionCellIndex - i];
                int distance = i;
                PlayParticlesAndSoundBasedOnDistance(existingCell, distance, false);
                yield return DamageActorOnCell(existingCell);
            }
            if (new CellXDistanceFromCasterToRightExists(TurnProcessorInst, LevelMasterInst, i, _caster.PositionCellIndex).Execute())
            {
                Cell existingCell = LevelMasterInst.Cells[_caster.PositionCellIndex + i];
                int distance = i;
                PlayParticlesAndSoundBasedOnDistance(existingCell, distance, true);
                yield return DamageActorOnCell(existingCell);
            }

            yield return _castPause;
        }

        yield return GlobalLowLevelConcrete.Pause;
    }

    private void PlayParticlesAndSoundBasedOnDistance(Cell cell, int distance, bool toRight)
    {
        WaveDirection direction = toRight ? WaveDirection.Right : WaveDirection.Left;
        WaveSize size;
        if (distance == 1)
        {
            size = WaveSize.Small;
            // Object.FindFirstObjectByType<AudioMaster>().PlaySound("shockWaveSmall");
        }  
        else if (distance == 2)
        {
            size = WaveSize.Medium;
            // Object.FindFirstObjectByType<AudioMaster>().PlaySound("shockWaveMedium");
        }
        else
        {
            size = WaveSize.Large;
            // Object.FindFirstObjectByType<AudioMaster>().PlaySound("shockWaveLarge");
        } 
        CellParticlePlayer.StartWave(cell, direction, size);
    }

    private IEnumerator DamageActorOnCell(Cell cell)
    {
        if (cell.EnityOccupyingThisCell != null)
        {
            yield return MovementLowLevelConcrete.StepArc
            (
                cell.EnityOccupyingThisCell, 
                Resources.Load<MoveData>("StepData"),
                cell.CellPosition, cell.CellPosition
            );
            // Object.FindFirstObjectByType<AudioMaster>().PlaySound("step");
            yield return cell.EnityOccupyingThisCell.TakeBluntDamage(Value);
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new ShockWaveConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, Value, _caster);
    }
}