using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RoomInstantiationInfo", menuName = "Rooms/RoomInstantiationInfo")]
public class RoomInstantiationInfo : ScriptableObject
{
    public List<EnemeyInstantiationInfo> EnemiesToInstantiate;
    public int PlayerCellIndex;
}