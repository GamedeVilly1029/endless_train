using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionConstructElement
{
    public List<bool> ConditionsToExecuteConcrete;
    public Func<DungeonMaster, IEnumerator> Concrete;

    public IEnumerator ExecuteConcrete(DungeonMaster dungoneMaster)
    {
        if (ConditionsToExecuteConcrete == null)
        {
            yield return Concrete(dungoneMaster);
        }
        else
        {
            foreach (bool condition in ConditionsToExecuteConcrete)
            {
                if (condition)
                {
                    Debug.Log("Some conditions are false => Concrete can't be executed");
                    yield break;
                }
            }
            yield return Concrete(dungoneMaster);
        }
    }
}