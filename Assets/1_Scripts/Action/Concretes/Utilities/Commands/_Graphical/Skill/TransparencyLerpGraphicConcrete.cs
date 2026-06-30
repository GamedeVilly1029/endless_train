using System.Collections;
using UnityEngine;

public class TransparencyLerpGraphicConcrete : GraphicalCoroutineConcrete
{
    private float _target;
    private float _duration;
    private SpriteRenderer _sprite;

    public TransparencyLerpGraphicConcrete(float target, SpriteRenderer sprite, float duration)
    {
        _target = target;
        _sprite = sprite;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        float timePassed = 0;
        Color c = _sprite.color;
        float start = c.a;
        while (timePassed < _duration)
        {
            float normalizedTime = timePassed / _duration;
            c.a = Mathf.Lerp(start, _target, normalizedTime);
            _sprite.color = c;
            timePassed += Time.deltaTime;
            yield return null;
        }
        c.a = _target;
        _sprite.color = c;
    }
}