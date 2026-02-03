using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionConstructElement
{
    public List<bool> ConditionsToExecuteConcrete;
    public IEnumerator ConcreteCoroutine;

    public IEnumerator ExecuteConcrete(DungeonMaster master)
    {
        yield return master.StartCoroutine(ConcreteCoroutine);
    }
}