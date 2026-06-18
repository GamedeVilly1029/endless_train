using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehindConcrete : BaseConcrete
{
    private BaseActor _teleporter;
    private BaseActor _teleportBehindThis;
    public TeleportBehindConcrete
    (
        TurnProcessor turnProcessor, 
        LevelMaster levelMaster, 
        BaseAction actionOfThisConcrete, 
        List<IConditionCommand> actionPassedConditions, 
        ActionConcreteTag tag,
        BaseActor teleporter,
        BaseActor teleportBehindThis
    ):base(turnProcessor, levelMaster, actionOfThisConcrete, actionPassedConditions, tag)
    {
        _teleporter = teleporter;
        _teleportBehindThis = teleportBehindThis;
    }

    public override IEnumerator ChildExecute()
    {
        int _idxSubtracter = _teleporter.IsFacingRight() ? 1 : -1;
        float rotationAngle = _teleportBehindThis.IsFacingRight() ? -180 : 0;

        yield return GlobalLowLevelConcrete.Pause;

        if (new CellAtIdxIsEmpty(TurnProcessorInst, LevelMasterInst, _teleportBehindThis.PositionCellIndex - _idxSubtracter).Execute())
        {
            LevelMasterInst.Cells[_teleporter.PositionCellIndex].EnityOccupyingThisCell = null;
            _teleporter.TransformReference.position = LevelMasterInst.Cells[_teleportBehindThis.PositionCellIndex - _idxSubtracter].CellPosition;
            _teleporter.PositionCellIndex = _teleportBehindThis.PositionCellIndex - _idxSubtracter;
            _teleporter.GraphicTransform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }
        else
        {
            Debug.Log("Can't teleport behind");
        }

        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new TeleportBehindConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, _teleporter, _teleportBehindThis);
    }
}