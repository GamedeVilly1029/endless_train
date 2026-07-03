using System.Collections;
using UnityEngine;

public class ConfusionEffect : GraphicalCoroutineConcrete
{
    private BaseActor _caster;
    private float _duration;

    public ConfusionEffect(BaseActor caster, float duration)
    {
        _caster = caster;
        _duration = duration;
    }
    public override IEnumerator Execute()
    {
        yield return new GraphicTransformColorLerpConcrete(_caster, Color.darkGray, _duration).Execute();
    }
}