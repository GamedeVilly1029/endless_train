using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewFightUIStarter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _startFightButton;
    [SerializeField] private Animator _blackScreenOverlay;
    [HideInInspector] public UnityEvent OnStartFightButtonClick;

    WaitForSeconds _keepDarkenAnimation = new(1f);

    private void Awake()
    {
        _startFightButton.gameObject.SetActive(false);
    }

    public void StartFightButtonAppear()
    {
        _startFightButton.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(StartNewFight(eventData));
    }

    private IEnumerator DarkenScreen()
    {
        _blackScreenOverlay.Play("Darken");
        yield return null;

        yield return new WaitForSeconds(_blackScreenOverlay.GetCurrentAnimatorStateInfo(0).length);
    }

    private IEnumerator StartNewFight(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartNewFight"))
        {
            yield return DarkenScreen();
            _startFightButton.gameObject.SetActive(false);
            OnStartFightButtonClick.Invoke();
            yield return _keepDarkenAnimation;

            _blackScreenOverlay.Play("Undarken");
        }
    }
}