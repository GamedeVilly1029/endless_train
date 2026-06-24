using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : BaseActor
{
    public List<Summon> Summons = new();

    public override void ManifestDeath()
    {
        base.ManifestDeath();
        foreach (Summon summon in Summons)
        {
            summon.SummonerInst = null;
        }
    }
}