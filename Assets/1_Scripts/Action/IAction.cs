using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public interface IAction
{
    public DungeonMaster DungeonMasterInstance{get;set;}
    public IActor Actor{get;set;}
    public GameObject UIRepresentation{get;set;}
    public List<ActionConstructElement> ActionConstruct{get;set;}
    public List<Func<DungeonMaster, ActionConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory{get;set;}

    public IEnumerator ExecuteAction(DungeonMaster dungeonMaster);
    public void InitializeAction(IActor actor, DungeonMaster dungeonMaster);
    public IAction CloneAndInstantiateUI(Transform transform);
    public List<ActionConstructElement> CloneActionConstruct(IAction actionClone);
}