using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour ,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("QuitGame"))
        {
            Application.Quit();
        }
    }
}
