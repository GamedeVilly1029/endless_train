using System.Collections;
using UnityEngine;

public class BeforeHitSwing : GraphicalCoroutineConcrete
{
    private BaseActor _caster;
    private float _halfDuration;

    public BeforeHitSwing(BaseActor caster, float fullDuration)
    {
        _caster = caster;
        _halfDuration = fullDuration / 2;
    }

    public override IEnumerator Execute()
    {
        Vector3 startPos = _caster.GraphicTransform.position;
        Vector3 offset = new(0.2f, 0, 0);
        Vector3 target = _caster.IsFacingRight() ? startPos - offset : startPos + offset;

        yield return new PositionLerpUtility(_halfDuration, startPos, target, _caster.GraphicTransform).Execute();
        yield return new PositionLerpUtility(_halfDuration, target, startPos, _caster.GraphicTransform).Execute();
    }
}