using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTurnUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TurnMaster _turnMaster;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartTurnButton"))
        {
            _turnMaster.OnEndTurn.Invoke();
        }
    }
}