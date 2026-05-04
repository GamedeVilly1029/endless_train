using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public interface IAction
{
    IAction PrototypeAction{get;set;}
    TurnProcessor TurnProcessorInstance{get;set;}
    LevelMaster LevelMasterInstance{get;set;}
    IActor Actor{get;set;}
    GameObject UIRepresentation{get;set;}
    List<IConcrete> ActionConstruct{get;set;}
    List<IConcrete> TurnTemporarySuccessfulConcreteHistory{get;set;}
    int CooldownMax{get;set;}
    int Cooldown{get;set;}

    IEnumerator ExecuteAction();
    void InitializeAction(IActor actor, TurnProcessor turnProcessor, LevelMaster levelMaster);
    IAction CloneAndInstantiateUI(Transform transform, IAction actionCloneReference);
    IAction CreateClone(Transform transform);
    void SetReferenceAction(IAction actionCloneReference, IAction actionClone);
    List<IConcrete> CloneActionConstruct(IAction actionClone);
}