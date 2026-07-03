using System.Collections;
using UnityEngine;

public class PositionLerpUtility : GraphicalCoroutineConcrete
{
    private float _duration;
    private Vector3 _start;
    private Vector3 _end;
    private Transform _transform;

    public PositionLerpUtility(float duration, Vector3 start, Vector3 end, Transform transform)
    {
        _duration = duration;
        _start = start;
        _end = end;
        _transform = transform;
    }

    public override IEnumerator Execute()
    {
        float timePast = 0;
        while (timePast <= _duration)
        {
            float t = timePast / _duration;
            _transform.position = Vector3.Lerp(_start, _end, t);
            timePast += Time.deltaTime;
            yield return null;
        }
        _transform.position = _end;
    }
}