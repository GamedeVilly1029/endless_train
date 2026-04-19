using System.Collections;
using UnityEngine;

public static class PushConcrete
{
    public static IEnumerator Push(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ParticlePlayer.StartPush(dungeonMaster.CurrentActor);
        if (CellBasedCondition.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
        {
            IActor actorAhead = GlobalLowLevelConcrete.TryReturnActorAhead(dungeonMaster, element);
            if (actorAhead != null)
            {
                if (CellBasedCondition.AdjacentCellsExists(dungeonMaster, actorAhead))
                {
                    yield return MovementLowLevelConcrete.BePushed(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight());
                }
            }
        }
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return GlobalLowLevelConcrete.Pause;
        ParticlePlayer.StopPush(dungeonMaster.CurrentActor);
    }
}
