using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConcrete
{
    IEnumerator Execute();
    // IConcrete Clone(IAction clonedAction, IConcrete concreteToClone);

    IConcrete Clone(IAction clonedAction);
}