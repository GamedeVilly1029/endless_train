using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionRow : MonoBehaviour
{
    [SerializeField] private BaseActor _actor;
    [SerializeField] private float offsetOfThePanel;

    public RectTransform Panel;
    [HideInInspector] public List<IAction> Actions;
    [HideInInspector] public UnityEvent OnActionAdd;
    [HideInInspector] public UnityEvent OnActionRemove;

    private Vector2 _actorPositionScreen;

    private void Awake()
    {
        Actions = new();
        OnActionAdd.AddListener(()=> IncreasePanelSize());
        OnActionRemove.AddListener(()=> ReducePanelSize());
    }

    void Update()
    {
        _actorPositionScreen = Camera.main.WorldToScreenPoint(_actor.Transform.position);
        Panel.position = new Vector2(_actorPositionScreen.x, _actorPositionScreen.y + offsetOfThePanel);
    }

    private void IncreasePanelSize()
    {
        if (Panel.sizeDelta == new Vector2(130, 0))
        {
            Panel.sizeDelta += new Vector2(0, 130);
        }
        else
        {
            Panel.sizeDelta += new Vector2(0, 120);
        }
    }

    private void ReducePanelSize()
    {
        if (Panel.sizeDelta == new Vector2(130, 130))
        {
            Panel.sizeDelta -= new Vector2(0, 130);
        }
        else
        {
            Panel.sizeDelta -= new Vector2(0, 120);
        }
    }
}