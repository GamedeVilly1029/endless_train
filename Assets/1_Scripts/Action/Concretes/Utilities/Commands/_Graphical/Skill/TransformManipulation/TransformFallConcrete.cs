using System.Collections;
using UnityEngine;

public class TransformFallConcrete : GraphicalCoroutineConcrete
{
    private Transform _caster;
    private float _duration;

    public TransformFallConcrete(Transform caster, float duration)
    {
        _caster = caster;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        Vector2 fallTarget = new Vector2(_caster.position.x, _caster.position.y - 0.75f);
        while (timePassed < _duration)
        {
            float t = timePassed / _duration;
            _caster.position = Vector2.Lerp(_caster.position, fallTarget, t);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }
        _caster.position = fallTarget;
    }
}