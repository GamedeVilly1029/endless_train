using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTurnUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TurnMaster _turnMaster;
    [SerializeField] private UIMaster _uIMaster;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uIMaster.InteractionBlock)
        {
            return;
        }

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartTurnButton"))
        {
            _turnMaster.OnEndTurn.Invoke();
        }
    }
}