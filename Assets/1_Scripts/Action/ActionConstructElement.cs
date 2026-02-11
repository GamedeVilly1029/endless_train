using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionConstructElement
{
    public List<bool> ConditionsToExecuteConcrete;
    public Func<DungeonMaster, IEnumerator> Concrete;

    public IEnumerator ExecuteConcrete(DungeonMaster master)
    {
        yield return Concrete(master);
    }
}