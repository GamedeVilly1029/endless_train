using System;
using UnityEngine;

public class AnimationCurveMove : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [Range(0, 1)] [SerializeField] private float _deltaTime;

    private void Start()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 3, transform.position.y, 0), _deltaTime);
    }
}
