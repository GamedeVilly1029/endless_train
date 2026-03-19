using UnityEngine;

[CreateAssetMenu(fileName = "MoveData", menuName = "Scriptable Objects/MoveData")]
public class MoveData : ScriptableObject
{
    public float Duration;
    public AnimationCurve Curve;
}
