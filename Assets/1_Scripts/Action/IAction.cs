using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public interface IAction
{
    IAction ActionCloneReference{get;set;}
    DungeonMaster DungeonMasterInstance{get;set;}
    IActor Actor{get;set;}
    GameObject UIRepresentation{get;set;}
    List<IConstructElement> ActionConstruct{get;set;}
    List<Func<DungeonMaster, IConstructElement, IEnumerator>> TurnTemporarySuccessfulConcreteHistory{get;set;}
    int CooldownMax{get;set;}
    int Cooldown{get;set;}

    IEnumerator ExecuteAction(DungeonMaster dungeonMaster);
    void InitializeAction(IActor actor, DungeonMaster dungeonMaster);
    IAction CloneAndInstantiateUI(Transform transform, IAction actionCloneReference);
    IAction CreateClone(Transform transform);
    void SetReferenceAction(IAction actionCloneReference, IAction actionClone);
    List<IConstructElement> CloneActionConstruct(IAction actionClone);
}