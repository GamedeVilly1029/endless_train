using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor 
{
    public List<BaseAction> Belt = new();
    public Transform BeltPanel;
    public int ActionInRowLimit = 3;
    public int ActionInRowCount = 0;

    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        TransformReference.position = LevelMasterInst.Cells[cellIndex].CellPosition;
        LevelMasterInst.Cells[cellIndex].EnityOccupyingThisCell = this;
        PositionCellIndex = cellIndex;
        MaxHP = HP;
        CurrentHP = MaxHP - 30;
        GraphicTransform.rotation = Quaternion.Euler(0f, YRotation, 0f);
    } 
}