using System.Collections;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewFightUIStarter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _startFightButton;
    [SerializeField] private Animator _blackScreenOverlay;
    public TextMeshProUGUI DescriptiveText;
    [HideInInspector] public UnityEvent OnStartFightButtonClick;
    [SerializeField] private UIMaster _uIMaster;

    WaitForSeconds _keepDarkenAnimation = new(1f);

    private void Awake()
    {
        _startFightButton.gameObject.SetActive(false);
        DescriptiveText.gameObject.SetActive(false);
    }

    public void StartFightButtonAppear()
    {
        _startFightButton.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uIMaster.InteractionBlock)
        {
            return;
        }
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
            DescriptiveText.gameObject.SetActive(false);
        }
    }

    public string GenerateDescription(int idx)
    {
        if (idx == 0)
        {
            return "You've got: 'Kick back' action! \n It strikes behind you current view direction and deals 7 damage if enemy is presented";
        }
        else if (idx == 1)
        {
            return "You've got: 'Slingshot' action! \n It launches a rock into you current view direction and deals 10 damage if enemy is presented";
        }
        else if (idx == 2)
        {
            return "You've got: 'Kick push' action! \n It stikes ahead of you dealing 5 dmg and then pushes the enemy one cell by your view direction";
        }
        else if (idx == 3)
        {
            return "You've got: 'Leg kick' action! \n It stikes ahead of you dealing 5 dmg, X2 if that's your only action in turn";
        }
        else
        {
            return "Index of addional action is out of range error";
        }
    }
}