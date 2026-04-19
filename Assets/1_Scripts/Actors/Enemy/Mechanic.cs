using TMPro;
using UnityEngine;

public class Mechanic : BaseActor
{
    public override void Initialize()
    {
        TransformReference.position = DungeonMasterInstance.Cells[5].CellPosition;
        DungeonMasterInstance.Cells[5].EnityOccupyingThisCell = this;
        PositionCellIndex = 5;
        StatusEffectsDuringTurn = new();
        StatusEffectsBeforeTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 50;
        CurrentHP = MaxHP;
        TransformReference.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        HPBarText.text = CurrentHP.ToString();
        TryToDie(CurrentHP);
    }
}