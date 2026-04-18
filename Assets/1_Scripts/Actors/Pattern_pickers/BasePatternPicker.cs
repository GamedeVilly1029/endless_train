using UnityEngine;
using System.Collections.Generic;

public class BasePatternPicker : MonoBehaviour
{
    public virtual void FillActionRowOrBelt()
    {
        Debug.LogWarning("Base pattern picker's fill method was called");
    }
    public virtual void InitializeActionPrototypes()
    {
        Debug.LogWarning("Base pattern picker's Initialize() method was called");
    }
}