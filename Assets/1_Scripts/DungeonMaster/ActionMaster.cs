using UnityEngine;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private TurnProcessor _turnProcessor;
    [SerializeField] private LevelMaster _levelMaster;

    private void Start()
    {
        foreach (IActor actor in _levelMaster.AllActors)
        {
            actor.PatternPicker.InitializeActionPrototypes();
        }
    }
}