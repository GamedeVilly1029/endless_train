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
            if (objectWasClicked.transform.parent == master.PlayerActor.BeltPanel)
            {
                master.PlayerActor.ActionRow.Add(master.PlayerActor.Belt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(master.PlayerActor.ActionRowPanel);
                master.PlayerActor.Belt.Remove(master.PlayerActor.Belt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
            }
            else if (objectWasClicked.transform.parent == master.PlayerActor.ActionRowPanel)
            {
                master.PlayerActor.Belt.Add(master.PlayerActor.ActionRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                master.PlayerActor.ActionRow.Remove(master.PlayerActor.ActionRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(master.PlayerActor.BeltPanel);
            }
        }
    }
}