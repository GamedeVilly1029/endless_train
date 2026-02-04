using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster : MonoBehaviour
{
    [SerializeField] private DungeonMaster master;
    
    // private Action CreateAction(IEnumerator concrete, GameObject UIRepresentationPrefab)
    // {
    //     Action action = new();
    //     action.ActionConstruct = new();
    //     ActionConstructElement constructElement = new();
    //     constructElement.ConcreteCoroutine = concrete;
    //     action.ActionConstruct.Add(constructElement);

    //     action.UIRepresentation = Instantiate(UIRepresentationPrefab, BeltPanel.transform);
    //     action.Actor = this;
    //     return action;
    // }
}