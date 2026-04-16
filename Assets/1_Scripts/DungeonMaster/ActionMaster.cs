using UnityEngine;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster _dungeonMaster;

    private void Start()
    {
        foreach (IActor actor in _dungeonMaster.AllActors)
        {
            actor.PatternPicker.InitializeActionPrototypes();
        }
    }
}