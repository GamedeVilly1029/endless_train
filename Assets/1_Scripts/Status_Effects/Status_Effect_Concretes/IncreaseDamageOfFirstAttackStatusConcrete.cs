using System.Collections;
using UnityEngine;

public class IncreaseDamageOfFirstAttackStatusConcrete : ValueStatusConcrete
{
    public IncreaseDamageOfFirstAttackStatusConcrete(
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        IActor actor,
        int value
    ) : base(turnProcessor, levelMaster, actor, value)
    {
    }

    public override IEnumerator ChildExecute()
    {
        ValueConcrete toEnhance =
            StatusEffectLowLevelFunctionality.ReturnAttackConcrete(
                StatusEffectLowLevelFunctionality.ExtractActorActions(TurnProcessorInst.CurrentActor)
            );

        if (toEnhance != null)
        {
            yield return toEnhance.Value += Value;
        }

        yield return null;
    }
}