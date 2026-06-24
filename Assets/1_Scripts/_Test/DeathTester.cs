using System.Collections;
using UnityEngine;

public class DeathTester : MonoBehaviour
{
    [SerializeField] private Transform _graphicTransform;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _fallDuration; [SerializeField] private float _fadeDuration;

    private WaitForSeconds _beforeFadingOut = new WaitForSeconds(3f);
    private TransformData _start;
    private TransformData _target;

    public void Start()
    {
        _start = new TransformData(_graphicTransform.localPosition, _graphicTransform.localRotation);
        _target = CreateTransform(_graphicTransform);

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(DeathFall());
        // yield return _beforeFadingOut;

        // StartCoroutine(FadeOut());
    }

    public TransformData CreateTransform(Transform actor)
    {
        if (Mathf.Abs(Mathf.DeltaAngle(_graphicTransform.localEulerAngles.y, 180f)) < 1f)
        {
            return TargetDataCreator(1, actor);
        }
        else
        {
            return TargetDataCreator(-1, actor);
        }
    }

    private static TransformData TargetDataCreator(float zRotInvert, Transform actor)
    {
        TransformData data = new TransformData(actor.localPosition, actor.localRotation);

        Quaternion rot = data.Rotation;
        rot = Quaternion.Euler(new Vector3(data.Rotation.eulerAngles.x, data.Rotation.eulerAngles.y, 90));

        Vector3 pos = data.Position;
        pos.x += 0.2f * zRotInvert;
        pos.y -= 0.2f;

        data.Rotation = rot;
        data.Position = pos;
        return data;
    }

    private IEnumerator DeathFall()
    {

        float timePassed = 0;
        while (timePassed < _fallDuration)
        {
            float t = timePassed / _fallDuration;
            TransformData currentData = TransformData.Lerp(_start, _target, t);
            _graphicTransform.localPosition = currentData.Position;
            _graphicTransform.localRotation = currentData.Rotation;
            yield return null;
            timePassed += Time.deltaTime;
        }
        _graphicTransform.localPosition = _target.Position;
        _graphicTransform.localRotation = _target.Rotation;
    }

    public IEnumerator FadeOut()
    {
        float timePassed = 0;
        Color c = _sprite.color;
        float start = c.a;
        while (timePassed < _fadeDuration)
        {
            float normalizedTime = timePassed / _fadeDuration;
            c.a = Mathf.Lerp(start, 0, normalizedTime);
            _sprite.color = c;
            timePassed += Time.deltaTime;
            yield return null;
        }
        c.a = 0;
        _sprite.color = c;
    }
}