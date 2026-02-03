using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionRowBeltClickSwitcher : MonoBehaviour, IPointerDownHandler
{
    public ActionBeltController BeltPanel;
    public ActionRowController ExecutionRow;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("UIActionIcon"))
        {
            GameObject objectWasClicked = eventData.pointerCurrentRaycast.gameObject;
            if (objectWasClicked.transform.parent == BeltPanel.transform)
            {
                ExecutionRow.ActionsInRow.Add(BeltPanel.ActionsInBelt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(ExecutionRow.transform);
                BeltPanel.ActionsInBelt.Remove(BeltPanel.ActionsInBelt.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
            }
            else if (objectWasClicked.transform.parent == ExecutionRow.transform)
            {
                BeltPanel.ActionsInBelt.Add(ExecutionRow.ActionsInRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                ExecutionRow.ActionsInRow.Remove(ExecutionRow.ActionsInRow.FirstOrDefault(x => x.UIRepresentation == objectWasClicked));
                objectWasClicked.transform.SetParent(BeltPanel.transform);
            }
        }
    }
}