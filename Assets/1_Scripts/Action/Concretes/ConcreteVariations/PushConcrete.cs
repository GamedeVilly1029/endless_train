using System.Collections;
using UnityEngine;

public static class PushConcrete
{
    public static IEnumerator Push(DungeonMaster dungeonMaster, IConstructElement element)
    {
        ParticlePlayer.StartPush(dungeonMaster.CurrentActor);
        if (CellBasedCondition.CellAheadExists(dungeonMaster, dungeonMaster.CurrentActor))
        {
            IActor actorAhead = LowLevelConcrete.TryReturnActorAhead(dungeonMaster, element);
            if (actorAhead != null)
            {
                if (CellBasedCondition.AdjacentCellsExists(dungeonMaster, actorAhead))
                {
                    yield return ActorPosManipulation.BePushed(dungeonMaster, actorAhead, dungeonMaster.CurrentActor.IsFacingRight());
                }
            }
        }
        Object.FindFirstObjectByType<AudioMaster>().PlaySound("push");
        yield return LowLevelConcrete.Pause;
        ParticlePlayer.StopPush(dungeonMaster.CurrentActor);
    }
}
