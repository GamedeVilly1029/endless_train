using UnityEngine;
using System.Collections.Generic;

public class BasePatternPicker : MonoBehaviour
{
    protected BaseActor _actor;
    protected TurnProcessor _turnProcessor;
    protected LevelMaster _levelMaster;

    public void Initialize(BaseActor actor)
    {
        _actor = actor;
        _turnProcessor = _actor.TurnProcessorInst;
        _levelMaster = _actor.LevelMasterInst;
        InitializeChild();
    }

    public void FillActionRowOrBelt()
    {
        if (_actor.IsDead)
        {
            _actor.ActionRowInst.Actions.Clear();
            return;
        }
        ChildFillActionRowOrBelt();
    }

    public virtual void ChildFillActionRowOrBelt()
    {
        Debug.LogWarning("Base pattern picker's ChildFillActionRowOrBelt() method was called");
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
            copies.Add(copy);
        }
        return copies;
    }
}