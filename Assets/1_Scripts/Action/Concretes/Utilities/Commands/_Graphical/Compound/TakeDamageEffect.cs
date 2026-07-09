using System.Collections;
using UnityEngine;

public class TakeDamageEffect : GraphicalCoroutineConcrete
{
    private BaseActor _caster;
    private float _halfDuration;

    public TakeDamageEffect(BaseActor caster, float halfDuration)
    {
        _caster = caster;
        _halfDuration = halfDuration;
    }

    public override IEnumerator Execute()
    {
        Vector3 start = _caster.GraphicTransform.position;
        Vector3 offset = new(0, 0.2f, 0);
        Vector3 target = _caster.GraphicTransform.position + offset;

        // _caster.StartCoroutine(new GraphicTransformColorLerpConcrete(_caster, Color.darkRed, _halfDuration*2).Execute());
        yield return new PositionLerpUtility(_halfDuration, start, target, _caster.GraphicTransform).Execute();
        yield return new PositionLerpUtility(_halfDuration, target, start, _caster.GraphicTransform).Execute();
    }
}