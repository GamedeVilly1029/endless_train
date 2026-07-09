using UnityEngine;
using UnityEngine.EventSystems;

public class AmbienceSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _checkBox;
    public AudioDataTransfer Transfer;

    public void Start()
    {
        Transfer.ToggleAudio = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("ToggleCheckBox"))
        {
            if (Transfer.ToggleAudio)
            {
                _checkBox.gameObject.SetActive(false);
                Transfer.ToggleAudio = false;
            }
            else
            {
                _checkBox.gameObject.SetActive(true);
                Transfer.ToggleAudio = true;
            }
        }
    }
}