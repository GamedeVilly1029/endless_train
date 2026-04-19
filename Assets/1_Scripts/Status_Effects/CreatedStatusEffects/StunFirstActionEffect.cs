using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstActionEffect : IStatusEffect
{
    public List<IStatusEffectConstructElement> StatusConstruct{get;set;}
    public bool DestroyAfterApplication{get;set;}
    public void InitializeStatusEffect(DungeonMaster dungeonMaster)
    {
        StatusConstruct = new();

        ValueStatusEffectConstructElement elem1 = new(StatusEffectConcrete.IncreaseDamageOfFirstAttackConcrete, 2, dungeonMaster.Player);
        StatusConstruct.Add(elem1);

        DestroyAfterApplication = true;
    }

    public IEnumerator ApplyStatusEffect(DungeonMaster dungeonMaster)
    {
        foreach (IStatusEffectConstructElement element in StatusConstruct)
        {
            yield return element.ExecuteStatusConcrete(dungeonMaster);
        }
    }

    public void SelfDestroy(DungeonMaster dungeonMaster)
    {
        dungeonMaster.CurrentActor.StatusEffectsDuringTurn.Remove(this);
    }
}