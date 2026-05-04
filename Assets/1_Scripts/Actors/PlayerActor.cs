using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor 
{
    public List<IAction> Belt = new();
    public Transform BeltPanel;
    public int ActionInRowLimit = 3;
    public int ActionInRowCount = 0;

    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        TransformReference.position = LevelMasterInst.Cells[cellIndex].CellPosition;
        LevelMasterInst.Cells[cellIndex].EnityOccupyingThisCell = this;
        PositionCellIndex = cellIndex;
        MaxHP = HP;
        CurrentHP = MaxHP;
        TransformReference.rotation = Quaternion.Euler(0f, YRotation, 0f);
    } 


    private void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
    }
}