using UnityEngine;

public class BaseActionCommand
{
    public virtual void Execute()
    {
        Debug.LogWarning($"Called from {this.GetType()}");
    }
}
