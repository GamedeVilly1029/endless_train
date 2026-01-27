using UnityEngine;
using System.Collections.Generic;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementCellAnchors;
    public Transform Player;
    public float MovementSpeed;
    public List<Vector2> CellAnchorPositions;

    private void Start()
    {
        CreateCellPositionsDictionary();
        Player.position = CellAnchorPositions[0];
    }

    public void ExecuteSequenceOfActions()
    {
        StartCoroutine(ActionConcretes.WalkTowardsDestinationCell(this, 5));
    }

    private void CreateCellPositionsDictionary()
    {
        CellAnchorPositions = new();
        foreach (Transform transform in MovementCellAnchors)
        {
            CellAnchorPositions.Add(transform.position);
        }
    }
}