using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor 
{
    public List<BaseAction> Belt = new();
    public Transform BeltPanel;
    public PlayerBeltPatternPicker BeltPatternPicker;
    public int ActionInRowLimit = 3;
    public int ActionInRowCount = 0;

    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        base.InitializeChild(cellIndex, YRotation, HP);
        BeltPatternPicker = PatternPicker as PlayerBeltPatternPicker;
    } 

    public void SetupInNewRoom(int cellIndex, float YRotation)
    {
        TransformReference.position = LevelMasterInst.Cells[cellIndex].CellPosition;
        LevelMasterInst.Cells[cellIndex].EnityOccupyingThisCell = this;
        PositionCellIndex = cellIndex;
        GraphicTransform.rotation = Quaternion.Euler(0f, YRotation, 0f);
        StatusEffectsDuringTurn = new();
        StatusEffectsBeforeTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        PatternPicker.Initialize(this);
    }
}