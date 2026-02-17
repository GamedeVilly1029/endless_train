using UnityEngine;
using System.Collections.Generic;

public interface IAction
{
    public DungeonMaster DungeonMaster{get;set;}
    public IActor Actor{get;set;}
    public GameObject UIRepresentation{get;set;}
    public int ValueForActionConcrete{get;set;}
    public List<ActionConstructElement> ActionConstruct{get;set;}

    public void InitializeAction(IActor actor, int valueForActionConcrete, DungeonMaster dungeonMaster);
    public IAction CloneAndInstantiateUI(Transform transform);
    public List<ActionConstructElement> CloneActionConstruct();
}