using System.Collections;
using UnityEngine;

public class ScaleLerpUtility : GraphicalCoroutineConcrete
{
    private float _duration;
    private Transform _transform;
    private Vector3 _startScale;
    private Vector3 _targetScale;

    public ScaleLerpUtility(float duration, Transform transform, Vector3 startScale, Vector3 targetScale)
    {
        _duration = duration;
        _transform = transform;
        _startScale = startScale;
        _targetScale = targetScale;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        while (timePassed < _duration)
        {
            float t = timePassed / _duration;
            _transform.localScale = Vector3.Lerp(_startScale, _targetScale, t);
            timePassed += Time.deltaTime;
            yield return null;
        }
        _transform.localScale = _targetScale;
    }
}