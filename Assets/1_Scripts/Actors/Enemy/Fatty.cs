using UnityEngine;

public class Fatty : BaseActor
{
    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        TransformReference.position = LevelMasterInst.Cells[cellIndex].CellPosition;
        LevelMasterInst.Cells[cellIndex].EnityOccupyingThisCell = this;
        PositionCellIndex = cellIndex;
        MaxHP = HP;
        CurrentHP = MaxHP;
        GraphicTransform.rotation = Quaternion.Euler(0f, YRotation, 0f);

        Traits.Add(new UnPushAbleTrait());
    }
}