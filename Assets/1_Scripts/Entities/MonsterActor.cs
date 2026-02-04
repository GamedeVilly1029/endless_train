using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class MonsterActor : MonoBehaviour, IActor
{
    [SerializeField] private DungeonMaster master;
    [SerializeField] private Transform MonsterTransform;
    [SerializeField] private RectTransform HPBar;
    [SerializeField] private TextMeshProUGUI HPBarText;
    public int HP;

    public List<Action> ActionRow { get; set;} = new();

    private void Start()
    {
        MonsterTransform.position = master.Cells[3].CellPosition;
        master.Cells[3].IsOcupiedByEntity = true;

        HPBar.position = Camera.main.WorldToScreenPoint(new Vector3(MonsterTransform.position.x, MonsterTransform.position.y + 1, 0));
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

        return action;
    }
}