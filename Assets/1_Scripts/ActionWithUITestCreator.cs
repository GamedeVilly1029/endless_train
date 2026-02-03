using System.Collections;
using UnityEngine;

public class ActionWithUITestCreator : MonoBehaviour
{
    [SerializeField] private DungeonMaster _master;
    [SerializeField] private GameObject _movementActionUIPrefab;
    [SerializeField] private GameObject _attackActionUIPrefab;

    private void Start()
    {
        _master.ActionBeltController.ActionsInBelt.Add(CreateAction(ActionConcretes.WalkXTiles(_master, 2), _movementActionUIPrefab));
        _master.ActionBeltController.ActionsInBelt.Add(CreateAction(ActionConcretes.AttackEntityAhead(_master, 2), _attackActionUIPrefab));
    }

    private Action CreateAction(IEnumerator concrete, GameObject UIRepresentationPrefab)
    {
        Action action = new();
        action.ActionConstruct = new();
        ActionConstructElement constructElement = new();
        constructElement.ConcreteCoroutine = concrete;
        action.ActionConstruct.Add(constructElement);

        action.UIRepresentation = Instantiate(UIRepresentationPrefab, _master.ActionBeltController.transform);
        return action;
    }
}