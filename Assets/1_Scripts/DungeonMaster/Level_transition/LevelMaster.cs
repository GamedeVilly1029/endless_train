using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] private TurnProcessor _turnProcessor;
    [SerializeField] private List<RoomInstantiationInfo> _rooms;
    [SerializeField] private Transform _instantiationPlaceForEnemies;
    [SerializeField] private List<Transform> MovementCellAnchors;

    [HideInInspector] public List<Cell> Cells;
    public PlayerActor Player;
    public List<IActor> AllActors = new();

    private void Start()
    {
        AllActors.Add(Player);
        CreateCellPositionsDictionary();
        // InitializePlayer();

        LoadRoom(_rooms[0]); // With one spider
    }

    public void LoadRoom(RoomInstantiationInfo room)
    {
        Player.Initialize(room.PlayerCellIndex, 0, 99, _turnProcessor, this);
        LoadEnemies(room);
    }

    private void LoadEnemies(RoomInstantiationInfo room)
    {
        foreach (EnemeyInstantiationInfo enemyInfo in room.EnemiesToInstantiate)
        {
            IActor enemy = Instantiate(enemyInfo.ActorPrefab, _instantiationPlaceForEnemies);
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
                CellPosition = transform.position,
                EnityOccupyingThisCell = null
            };
            Cells.Add(cell);
        }
    }

    // private void InitializePlayer()
    // {
    //     foreach (IActor actor in AllActors)
    //     {
    //         if (actor is PlayerActor)
    //         {
    //             Player = actor as PlayerActor;
    //         }
    //     }
    //     if (Player == null)
    //     {
    //         Debug.Log("Player actor is null, fix that ;3");
    //     }
    // }
}