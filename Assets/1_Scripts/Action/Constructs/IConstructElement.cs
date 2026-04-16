using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IConstructElement
{
    public List<Func<DungeonMaster, IActor, bool>> ConditionsToExecuteConcrete{get;set;}
    public Func<DungeonMaster, IConstructElement, IEnumerator> Concrete{get;set;}
    public IAction ActionOfThisConcrete{get;set;}
    public ActionConcreteTag ConcreteTag{get;set;}

    public IEnumerator Execute(DungeonMaster dungeonMaster);
    public IConstructElement Clone(IAction actionClone);
}