using System.Collections;
using UnityEngine;

public class RotationLerpGraphicConcrete : GraphicalCoroutineConcrete
{
    private Transform _actorGraphicalTransform;
    private Quaternion _startRotation;
    private Quaternion _target;
    private float _duration;

    public RotationLerpGraphicConcrete(Transform actorGraphicalTransform, Quaternion startRotation, Quaternion target, float duration)
    {
        _actorGraphicalTransform = actorGraphicalTransform;
        _startRotation = startRotation;
        _target = target;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        while (timePassed < _duration)
        {
            float normalizedTime = timePassed / _duration;
            _actorGraphicalTransform.rotation = Quaternion.Lerp(_startRotation, _target, normalizedTime);
            timePassed += Time.deltaTime; 
            yield return null;
        }
        _actorGraphicalTransform.rotation = _target;
    }
}