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

    public IEnumerator ExecuteConcrete(DungeonMaster dungeonMaster)
    {
        if (ConditionsToExecuteConcrete == null)
        {
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(Concrete);
            yield return Concrete(dungeonMaster, this);
        }
        else
        {
            foreach (var condition in ConditionsToExecuteConcrete)
            {
                if (!condition(dungeonMaster, ActionOfThisConcrete.Actor))
                {
                    // Debug.Log($"condition: {condition.Method.Name}, interupts Concrete: {Concrete.Method.Name} from execution");
                    yield break;
                }
            }
            ActionOfThisConcrete.TurnTemporarySuccessfulConcreteHistory.Add(Concrete);
            yield return Concrete(dungeonMaster, this);
        }
    }
}