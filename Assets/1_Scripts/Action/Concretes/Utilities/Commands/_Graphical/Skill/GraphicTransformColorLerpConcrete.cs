using System.Collections;
using UnityEngine;

public class GraphicTransformColorLerpConcrete : GraphicalConcrete
{
    private BaseActor _caster;
    private Color _targetColor;
    private float _duration;

    public GraphicTransformColorLerpConcrete
    (
        BaseActor caster,
        Color targetColor,
        float duration
    )
    {
        _caster = caster;
        _targetColor = targetColor;
        _duration = duration;
    }

    public override void Execute()
    {
        SpriteRenderer sprt = _caster.GraphicTransform.GetComponent<SpriteRenderer>();
        Color startColor = sprt.color;

        _caster.StartCoroutine(Interpolate(sprt, startColor, _targetColor, _duration));
    }

    private IEnumerator Interpolate(SpriteRenderer sprt, Color start, Color target, float duration)
    {
        float passedTime = 0;
        while(passedTime <= duration)
        {
            float normalizedTime = passedTime / duration;
            sprt.color = Color.Lerp(start, target, normalizedTime);
            passedTime += Time.deltaTime;
            yield return null;
        }
        passedTime = 0;
        while(passedTime <= duration)
        {
            float normalizedTime = passedTime / duration;
            sprt.color = Color.Lerp(target, start, normalizedTime);
            passedTime += Time.deltaTime;
            yield return null;
        }
        sprt.color = start;
    }
}
