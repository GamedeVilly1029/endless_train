using System.Collections.Generic;
using UnityEngine;

public class ActorTrait
{
    public void SelfDestroy(List<ActorTrait> listToRemoveFrom)
    {
        listToRemoveFrom.Remove(this);
    }
}