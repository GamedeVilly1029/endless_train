using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ActionRowUI : MonoBehaviour
{
    [SerializeField] private BaseActor _actor;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float offsetOfThePanel;
    public UnityEvent OnActionAdd;
    public UnityEvent OnActionRemove;

    private Vector2 _actorPositionScreen;

    private void Awake()
    {
        OnActionAdd.AddListener(()=> IncreasePanelSize());
        OnActionRemove.AddListener(()=> ReducePanelSize());
    }

    void Update()
    {
        _actorPositionScreen = Camera.main.WorldToScreenPoint(_actor.Transform.position);
        _panel.position = new Vector2(_actorPositionScreen.x, _actorPositionScreen.y + offsetOfThePanel);
    }

    private void IncreasePanelSize()
    {
        _panel.sizeDelta += new Vector2(0f, 130f);
    }

    private void ReducePanelSize()
    {
        _panel.sizeDelta -= new Vector2(0f, 130f);
    }
}