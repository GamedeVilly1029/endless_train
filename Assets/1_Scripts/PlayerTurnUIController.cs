using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTurnUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DungeonMaster dungeonMaster;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartTurnButton"))
        {
            dungeonMaster.StartCoroutine(dungeonMaster.IterateThroughActionRow());
        }
    }
}