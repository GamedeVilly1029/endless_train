using UnityEngine;

public class Summon : BaseActor
{
    [HideInInspector] public Summoner SummonerInst;

    public void InitializeSummon(Summoner summoner)
    {
        SummonerInst = summoner;
        Debug.Log($"{this.SummonerInst}");
    }
}