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
        base.InitializeChild(cellIndex, YRotation, HP);
    } 
}