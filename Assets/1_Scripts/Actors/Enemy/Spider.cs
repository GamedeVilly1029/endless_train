using UnityEngine;

public class Spider : BaseActor
{
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
        HPBarText.text = CurrentHP.ToString();
        TryToDie(CurrentHP);
    }
}