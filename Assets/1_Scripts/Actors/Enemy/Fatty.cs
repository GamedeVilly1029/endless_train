using UnityEngine;

public class Fatty : BaseActor
{
    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        base.InitializeChild(cellIndex, YRotation, HP);

        Traits.Add(new UnPushAbleTrait());
    }
}