using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class MonsterActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster master;
    [SerializeField] private TextMeshPro HPBarText;

    public int HP;
    public Transform Transform{get {return transform;} set{}}
    public List<Action> ActionRow { get; set;} = new();

    private void Start()
    {
        Transform.position = master.Cells[3].CellPosition;
        master.Cells[3].IsOcupiedByEntity = true;

        HP = 10;

        ActionRow.Add(CreateAction(ActionConcretes.MoveOneCellForward(master)));
    }

    private void Update()
    {
        HPBarText.text = HP.ToString();
    }

    private Action CreateAction(IEnumerator concrete)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.ConcreteCoroutine = concrete;
        action.ActionConstruct.Add(constructElement);
        action.Actor = this;

        return action;
    }
}