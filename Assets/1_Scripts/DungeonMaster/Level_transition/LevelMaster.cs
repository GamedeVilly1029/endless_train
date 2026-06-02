using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] private TurnProcessor _turnProcessor;
    public List<RoomInstantiationInfo> Rooms;
    [SerializeField] private Transform _instantiationPlaceForEnemies;
    [SerializeField] private List<Transform> MovementCellAnchors;

    [HideInInspector] public List<Cell> Cells;
    public PlayerActor Player;
    public List<BaseActor> AllActors = new();
    private Queue<RoomInstantiationInfo> _roomQueue;

    private void Start()
    {
        AllActors.Add(Player);
        CreateCellPositionsDictionary();

        FillRoomQueue();
        LoadNextRoom();
    }

    public void LoadNextRoom()
    {
        RoomInstantiationInfo room = _roomQueue.Dequeue();
        Player.Initialize(room.PlayerCellIndex, room.PlayerRotation, 99, _turnProcessor, this);
        LoadEnemies(room);
    }

    private void LoadEnemies(RoomInstantiationInfo room)
    {
        foreach (EnemyInstantiationInfo enemyInfo in room.EnemiesToInstantiate)
        {
            BaseActor enemy = Instantiate(enemyInfo.ActorPrefab, _instantiationPlaceForEnemies);
            enemy.Initialize(enemyInfo.CellIndex, enemyInfo.RotationAngle, enemyInfo.HP, _turnProcessor, this);
            AllActors.Add(enemy);
        }
    }

    private void CreateCellPositionsDictionary()
    {
        Cells = new();
        foreach (Transform transform in MovementCellAnchors)
        {
            Cell cell = new()
            {
                CellTransform = transform,
                CellPosition = transform.position,
                EnityOccupyingThisCell = null
            };
            Cells.Add(cell);
        }
    }

    public bool AliveEnemiesPresented()
    {
        foreach (BaseActor actor in AllActors)
        {
            if (actor is not PlayerActor && !actor.IsDead)
            {
                return true;
            }
        }
        return false;
    }

   private void FillRoomQueue()
    {
        _roomQueue = new();
        foreach(RoomInstantiationInfo room in Rooms)
        {
            _roomQueue.Enqueue(room);
        }
    }
}