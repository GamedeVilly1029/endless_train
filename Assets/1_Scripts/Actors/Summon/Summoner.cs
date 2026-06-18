using System.Collections.Generic;
using UnityEngine;

public class Summoner : BaseActor
{
    public List<Summon> Summons = new();

    public override void Die()
    {
        foreach (Summon summon in Summons)
        {
            summon.SummonerInst = null;
        }
        base.Die();
    }
}