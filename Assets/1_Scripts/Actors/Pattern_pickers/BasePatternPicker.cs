using UnityEngine;
using System.Collections.Generic;

public class BasePatternPicker : MonoBehaviour
{
    [SerializeField] protected BaseActor _actor;
    protected TurnProcessor _turnProcessor;
    protected LevelMaster _levelMaster;

    public void Initialize()
    {
        if(_actor != null)
        {
            _turnProcessor = _actor.TurnProcessorInst;
            _levelMaster = _actor.LevelMasterInst;
        }
        InitializeChild();
    }

    public virtual void FillActionRowOrBelt()
    {
        Debug.LogWarning("Base pattern picker's fill method was called");
    }
    public virtual void InitializeChild()
    {
        Debug.LogWarning("Base pattern picker's Initialize() method was called");
    }

    public List<BaseAction> CopyActionSet(List<BaseAction> set, RectTransform UIPanel)
    {
        List<BaseAction> copies = new();
        foreach (BaseAction action in set)
        {
            BaseAction copy = action.CloneAndInstantiateUI(UIPanel, action);
            copies.Add(copy); }
        return copies;
    }
}