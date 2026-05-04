using UnityEngine;
using System.Collections.Generic;

public class BasePatternPicker : MonoBehaviour
{
    [SerializeField] private BaseActor _actor;
    protected TurnProcessor _turnProcessor;
    protected LevelMaster _levelMaster;

    private void Start()
    {
        _turnProcessor = _actor.TurnProcessorInst;
        _levelMaster = _actor.LevelMasterInst;

        Debug.Log("Values of BasePatternPicker were assigned");
    }

    public virtual void FillActionRowOrBelt()
    {
        Debug.LogWarning("Base pattern picker's fill method was called");
    }
    public virtual void InitializeActionPrototypes()
    {
        Debug.LogWarning("Base pattern picker's Initialize() method was called");
    }
}