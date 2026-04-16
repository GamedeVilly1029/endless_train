using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConstructElement : IConstructElement
{
    public List<Func<DungeonMaster, IActor, bool>> ConditionsToExecuteConcrete{get;set;}
    public Func<DungeonMaster, IConstructElement, IEnumerator> Concrete{get;set;}
    public IAction ActionOfThisConcrete{get;set;}
    public ActionConcreteTag ConcreteTag{get;set;}

    public BaseConstructElement
    (
    IAction actionOfThisConcrete,
    List<Func<DungeonMaster, IActor, bool>> conditions, 
    Func<DungeonMaster, IConstructElement, IEnumerator> concrete, 
    ActionConcreteTag tag
    )
    {
        ActionOfThisConcrete = actionOfThisConcrete;
        ConditionsToExecuteConcrete = conditions;
        Concrete = concrete;
        ConcreteTag = tag;
    }

    public IEnumerator Execute(DungeonMaster dungeonMaster)
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

    public virtual IConstructElement Clone(IAction actionClone)
    {
        BaseConstructElement clone = new(
           actionClone,
           ConditionsToExecuteConcrete,
           Concrete,
           ConcreteTag
        );
        return clone;
    }
}