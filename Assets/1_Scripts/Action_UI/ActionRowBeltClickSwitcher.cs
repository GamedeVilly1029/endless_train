using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionRowBeltClickSwitcher : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private DungeonMaster master;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("UIActionIcon"))
        {
            GameObject objectWasClicked = eventData.pointerCurrentRaycast.gameObject;
            if (objectWasClicked.transform.parent == master.Player.BeltPanel)
            {
                master.Player.ActionRow.Add(master.Player.Belt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(master.Player.ActionRowPanel);
                master.Player.Belt.Remove(master.Player.Belt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
            }
            else if (objectWasClicked.transform.parent == master.Player.ActionRowPanel)
            {
                master.Player.Belt.Add(master.Player.ActionRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                master.Player.ActionRow.Remove(master.Player.ActionRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(master.Player.BeltPanel);
            }
        }
    }
}