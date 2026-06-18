using UnityEngine;

public class Summon : BaseActor
{
    [HideInInspector] public Summoner SummonerInst;

    public void InitializeSummon(Summoner summoner)
    {
        SummonerInst = summoner;
    }

    public override void Die()
    {
        if (SummonerInst != null)
        {
            if (SummonerInst.Summons.Contains(this))
            {
                SummonerInst.Summons.Remove(this);
            }
        }
        base.Die();
    }
}