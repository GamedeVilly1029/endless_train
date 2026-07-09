using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartNewRun : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartGame"))
        {
            SceneManager.LoadScene(0);
        }
    }
}