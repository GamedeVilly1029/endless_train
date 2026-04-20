using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstActionEffect : IStatusEffect
{
    public List<IStatusEffectConstructElement> StatusConstruct{get;set;}
    public bool DestroyAfterApplication{get;set;}

    private IAction _stunned;
    public void InitializeStatusEffect(DungeonMaster dungeonMaster)
    {
        StatusConstruct = new();
        _stunned = new BeStunned();
        _stunned.InitializeAction(dungeonMaster.Player, dungeonMaster);

        ActionAssignmentStatusConstructElement elem1 = new(StatusEffectConcrete.StunFirstPlayerAction, _stunned, dungeonMaster.Player);
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
        dungeonMaster.CurrentActor.StatusEffectsBeforeTurn.Remove(this);
    }
}