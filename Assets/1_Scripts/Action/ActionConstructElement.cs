using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionConstructElement
{
    public List<Func<DungeonMaster, IActor, bool>> ConditionsToExecuteConcrete;
    public Func<DungeonMaster, ActionConstructElement, IEnumerator> Concrete;
    public int ConcreteValue;
    public IAction ActionOfThisConcrete;
    public ActionConcreteTag ConcreteTag;

    public ActionConstructElement(IAction actionOfThisConcrete,
    List<Func<DungeonMaster, IActor, bool>> conditions, 
    Func<DungeonMaster, ActionConstructElement, IEnumerator> concrete, 
    int concreteValue,
    ActionConcreteTag tag
    )
    {
        ActionOfThisConcrete = actionOfThisConcrete;
        ConditionsToExecuteConcrete = conditions;
        Concrete = concrete;
        ConcreteValue = concreteValue;
        ConcreteTag = tag;
    }

    public IEnumerator ExecuteConcrete(DungeonMaster dungoneMaster)
    {
        if (ConditionsToExecuteConcrete == null)
        {
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(Concrete);
            yield return Concrete(dungoneMaster, this);
        }
        else
        {
            foreach (var condition in ConditionsToExecuteConcrete)
            {
                if (!condition(dungoneMaster, ActionOfThisConcrete.Actor))
                {
                    Debug.Log("Some conditions are false => Concrete can't be executed");
                    yield break;
                }
            }
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(Concrete);
            yield return Concrete(dungoneMaster, this);
        }
    }
}