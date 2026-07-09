using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] private TurnProcessor _turnProcessor;
    public List<RoomInstantiationInfo> Rooms;
    public Transform InstantiationPlaceForEnemies;
    [SerializeField] private List<Transform> MovementCellAnchors;

    [HideInInspector] public List<Cell> Cells;
    public PlayerActor Player;
    public List<BaseActor> AllActors = new();
    private Queue<RoomInstantiationInfo> _roomQueue;
    private int _initialRoomCount;
    public Queue<int> PlayerAddActionIdxQueue;

    private void Start()
    {
        AllActors.Add(Player);
        CreateCellPositionsDictionary();

        FillRoomQueue();
        _initialRoomCount = Rooms.Count;
        PlayerAddActionIdxQueue = GenerateAdditionalActionSequenceIndexes();
        LoadNextRoom();
    }

    public void LoadNextRoom()
    {
        RoomInstantiationInfo room = _roomQueue.Dequeue();
        Cells[Player.PositionCellIndex].EnityOccupyingThisCell = null;
        if (_roomQueue.Count == _initialRoomCount - 1)
        {
            Player.Initialize(room.PlayerCellIndex, room.PlayerRotation, 50, _turnProcessor, this);
        }
        else
        {
            Player.SetupInNewRoom(room.PlayerCellIndex, room.PlayerRotation);
        }
        LoadEnemies(room);
    }

    private void LoadEnemies(RoomInstantiationInfo room)
    {
        foreach (EnemyInstantiationInfo enemyInfo in room.EnemiesToInstantiate)
        {
            BaseActor enemy = Instantiate(enemyInfo.ActorPrefab, InstantiationPlaceForEnemies);
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

    private Queue<int> GenerateAdditionalActionSequenceIndexes()
    {
        int[] indexes = Enumerable.Range(0, Rooms.Count - 1).ToArray();
        for (int i = indexes.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (indexes[j], indexes[i]) = (indexes[i], indexes[j]);
        }

        return new(indexes);
    }
}