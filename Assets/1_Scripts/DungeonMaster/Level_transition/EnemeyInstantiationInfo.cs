using System;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemeyInstantiationInfo", menuName = "Rooms/EnemeyInstantiationInfo")]
public class EnemeyInstantiationInfo : ScriptableObject
{
    public BaseActor ActorPrefab;
    public int CellIndex;
    public int HP;
    public float RotationAngle;
}
