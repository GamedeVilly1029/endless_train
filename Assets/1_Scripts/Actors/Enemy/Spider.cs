using UnityEngine;

public class Spider : BaseActor
{
    public override void Initialize()
    {
        TransformReference.position = DungeonMasterInstance.Cells[9].CellPosition;
        DungeonMasterInstance.Cells[9].EnityOccupyingThisCell = this;
        PositionCellIndex = 9;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 25;
        CurrentHP = MaxHP;
        TransformReference.rotation = Quaternion.Euler(0f, -180f, 0f);
    }

    private void Update()
    {
        HPBarText.text = CurrentHP.ToString();
        TryToDie(CurrentHP);
    }
}