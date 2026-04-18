using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor 
{
    public List<IAction> Belt = new();
    public Transform BeltPanel;
    public int ActionInRowLimit = 3;
    public int ActionInRowCount = 0;

    public override void Initialize()
    {
        TransformReference.position = DungeonMasterInstance.Cells[9].CellPosition;
        DungeonMasterInstance.Cells[9].EnityOccupyingThisCell = this;
        PositionCellIndex = 9;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 99;
        CurrentHP = MaxHP;
        TransformReference.rotation = Quaternion.Euler(0f, 180f, 0f);
    }


    private void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
    }
}