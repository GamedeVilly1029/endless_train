using UnityEngine;
using System.Collections;

public class ActorDeathFall : GraphicalCoroutineConcrete
{
    private BaseActor _actor;
    private TransformData _start;
    private TransformData _target;
    private float _fallDuration;

    public ActorDeathFall(BaseActor actor, float fallDuration, TransformData start, TransformData target)
    {
        _actor = actor;
        _fallDuration = fallDuration;
        _start = start;
        _target = target;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        while (timePassed < _fallDuration)
        {
            float t = timePassed / _fallDuration;
            TransformData currentData = TransformData.Lerp(_start, _target, t);
            _actor.GraphicTransform.localPosition = currentData.Position;
            _actor.GraphicTransform.localRotation = currentData.Rotation;
            yield return null;
            timePassed += Time.deltaTime;
        }
        _actor.GraphicTransform.localPosition = _target.Position;
        _actor.GraphicTransform.localRotation = _target.Rotation;
    }
}