using System.Collections;
using UnityEngine;

public class GraphicalCoroutineConcrete
{
    public virtual IEnumerator Execute()
    {
        Debug.LogWarning("Basic implemention called, returning");
        yield break;
    }
}