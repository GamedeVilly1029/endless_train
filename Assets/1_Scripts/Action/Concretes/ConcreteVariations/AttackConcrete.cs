using System.Collections;
using UnityEngine;

public static class AttackConcrete
{
    
    public static IEnumerator StrikeConcrete(DungeonMaster dungeonMaster, IConstructElement element)
    {
        yield return AttackLowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
    }

    public static IEnumerator TantrumStrike(DungeonMaster dungeonMaster, IConstructElement element)
    {
        for (int i = dungeonMaster.CurrentActor.FightBasedActionHistory.Count - 1; i >= 0; i--)
        {
            if (dungeonMaster.CurrentActor.FightBasedActionHistory[i] is Tantrum)
            {
                yield return AttackLowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
            }
            else
            {
                break;
            }
        }
        yield return AttackLowLevelConcrete.AttackEntityAhead(dungeonMaster, element);
    }
}