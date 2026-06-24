using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseActorDeathPlayer : GraphicalCoroutineConcrete
{
    private BaseActor _actor;
    private float _fadeDuration;
    private float _fallDuration;
    private WaitForSeconds _beforeFadingOut = new WaitForSeconds(0.5f);

    public BaseActorDeathPlayer(BaseActor actor, float fadeDuration, float fallDuration)
    {
        _actor = actor;
        _fadeDuration = fadeDuration;
        _fallDuration = fallDuration;
    }

    public override IEnumerator Execute()
    {
        TransformData start = new TransformData(_actor.GraphicTransform.localPosition, _actor.GraphicTransform.localRotation);
        TransformData target = DeathTransformDataCreator.CreateTransform(_actor);

        _actor.StartCoroutine(new ActorDeathFall(_actor, _fallDuration, start, target).Execute());
        yield return _beforeFadingOut;

        yield return _actor.StartCoroutine(new TransparencyLerpGraphicConcrete(0, _actor.SpriteRend, _fadeDuration).Execute());
    }
}