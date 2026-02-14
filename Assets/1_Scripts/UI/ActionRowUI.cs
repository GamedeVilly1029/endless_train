using UnityEngine;

public class ActionRowUI : MonoBehaviour
{
    [SerializeField] private Transform _actor;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float offsetOfThePanel;

    private Vector2 _actorPositionScreen;

    void Update()
    {
        _actorPositionScreen = Camera.main.WorldToScreenPoint(_actor.position);
        _panel.position = new Vector2(_actorPositionScreen.x, _actorPositionScreen.y + offsetOfThePanel);
    }
}