using System.Collections;
using UnityEngine;

public class TakeDamageStatusConcrete : ValueStatusConcrete
{
    public TakeDamageStatusConcrete(
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        BaseActor actor,
        int value
    ) : base(turnProcessor, levelMaster, actor, value)
    {
    }

    public override IEnumerator ChildExecute()
    {
        yield return Actor.TakePiercingDamage(Value);
    }
}