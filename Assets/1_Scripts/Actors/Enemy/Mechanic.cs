using TMPro;
using UnityEngine;

public class Mechanic : BaseActor, IMonster
{
    public MonsterTypes MonsterType {get;set;}

    public new void Initialize()
    {
        DungeonMasterInstance.MonstersWithActorReference.Add(this, this);
        DungeonMasterInstance.AllActors.Add(this);
        Transform.position = DungeonMasterInstance.Cells[9].CellPosition;
        DungeonMasterInstance.Cells[9].EnityOccupyingThisCell = this;
        PositionCellIndex = 9;
        IsFacingRight = true;
        MonsterType = MonsterTypes.glist1;
        StatusEffectsForTurn = new();
        StatusEffectsBeforeTakingDamage = new();
        MaxHP = 99;
        CurrentHP = MaxHP;
    }

    private void Update()
    {
        HPBarText.text = CurrentHP.ToString();
        TryToDie(CurrentHP);
        SpriteRend.flipX = !IsFacingRight;
    }
}