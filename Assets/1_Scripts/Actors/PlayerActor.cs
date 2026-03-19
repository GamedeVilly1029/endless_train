using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor 
{
    public List<IAction> Belt = new();
    public Transform BeltPanel;

    public new void Initialize()
    {
        DungeonMasterInstance.AllActors.Add(this);
        Transform.position = DungeonMasterInstance.Cells[4].CellPosition;
        DungeonMasterInstance.Cells[4].EnityOccupyingThisCell = this;
        PositionCellIndex = 4;
        IsFacingRight = true;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 99;
        CurrentHP = MaxHP;
    }


    private void Update()
    {
        TryToDie(CurrentHP);
        HPBarText.text = CurrentHP.ToString();
        SpriteRend.flipX = !IsFacingRight;
    }
}