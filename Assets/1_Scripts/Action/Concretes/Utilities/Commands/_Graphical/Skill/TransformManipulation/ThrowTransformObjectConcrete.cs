using System.Collections;
using UnityEngine;

public class ThrowTransformObjectConcrete : GraphicalCoroutineConcrete
{
    private Transform _throwable;
    private Vector2 _target;
    private float _duration;

    public ThrowTransformObjectConcrete(Transform throwable, Vector2 target, float duration)
    {
        _throwable = throwable;
        _target = target;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        Vector2 initPos = _throwable.position;
        while (timePassed < _duration)
        {
            float t = timePassed / _duration;
            _throwable.position = Vector2.Lerp(initPos, _target, t);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }
        _throwable.position = _target;
    }
}