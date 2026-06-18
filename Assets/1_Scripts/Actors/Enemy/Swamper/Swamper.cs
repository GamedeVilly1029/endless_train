using UnityEngine;

public class Swamper : Summoner
{
    public override void InitializeChild(int cellIndex, float YRotation, int HP)
    {
        base.InitializeChild(cellIndex, YRotation, HP);

        // BaseStatusEffect regen = new RegenerateEffect();
        // regen.Initialize(TurnProcessorInst, LevelMasterInst, this);
        // StatusEffectsBeforeTurn.Add(regen);

        Traits.Add(new UnPushAbleTrait());
    }
}