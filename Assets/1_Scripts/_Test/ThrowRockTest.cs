using System.Collections;
using UnityEngine;

public class ThrowRockTest : MonoBehaviour
{
    public Transform Caster;
    public Transform Target;
    public float Duration;
    public float FadeDuration;
    public SpriteRenderer Rend;

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return Throw();
        StartCoroutine(Decay());
        StartCoroutine(Fall());
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.5f);
        float timePassed = 0;
        while (timePassed < Duration)
        {
            float t = timePassed / Duration;
            Caster.position = Vector2.Lerp(Caster.position, Target.position, t);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }
        Caster.position = Target.position;
    }

    private IEnumerator Decay()
    {
        float timePassed = 0;
        Color c = Rend.color;
        float start = c.a;
        while (timePassed < FadeDuration)
        {
            float normalizedTime = timePassed / FadeDuration;
            c.a = Mathf.Lerp(start, 0, normalizedTime);
            Rend.color = c;
            timePassed += Time.deltaTime;
            yield return null;
        }
        c.a = 0;
        Rend.color = c;
    }

    private IEnumerator Fall()
    {
        float timePassed = 0;
        Vector2 fallTarget = new Vector2(Caster.position.x, Caster.position.y - 1);
        while (timePassed < Duration)
        {
            float t = timePassed / Duration;
            Caster.position = Vector2.Lerp(Caster.position, fallTarget, t);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }
        Caster.position = fallTarget;
    }
}