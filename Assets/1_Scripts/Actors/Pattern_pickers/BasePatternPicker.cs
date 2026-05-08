using UnityEngine;
using System.Collections.Generic;

public class BasePatternPicker : MonoBehaviour
{
    [SerializeField] private BaseActor _actor;
    protected TurnProcessor _turnProcessor;
    protected LevelMaster _levelMaster;

    public void Initialize()
    {
        if(_actor != null)
        {
            _turnProcessor = _actor.TurnProcessorInst;
            _levelMaster = _actor.LevelMasterInst;
        }
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