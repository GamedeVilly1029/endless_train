using System.Collections;
using UnityEngine;

public class SpawnGrowEffect : GraphicalCoroutineConcrete
{
    private BaseActor _actor;
    private float _duration;

    public SpawnGrowEffect(BaseActor actor, float duration)
    {
        _actor = actor;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 normalScale = _actor.GraphicTransform.localScale;

        _actor.GraphicTransform.localScale = startScale;
        float elapsed = 0;
        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            _actor.GraphicTransform.localScale = Vector3.Lerp(startScale, normalScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _actor.GraphicTransform.localScale = normalScale;
    }
}