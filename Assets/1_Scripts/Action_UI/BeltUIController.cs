using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeltUIController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private ActionRowUI _playerActionRow;

    private GameObject _objectWasClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("UIActionIcon"))
        {
            _objectWasClicked = eventData.pointerCurrentRaycast.gameObject;
            if (_objectWasClicked.transform.parent == _dungeonMaster.Player.BeltPanel)
            {
                FromBeltToRow();
                _playerActionRow.OnActionAdd.Invoke();
            }
            else if (_objectWasClicked.transform.parent == _dungeonMaster.Player.ActionRowPanel)
            {
                FromRowToBelt();
                _playerActionRow.OnActionRemove.Invoke();
            }
        }
    }

    private void FromBeltToRow()
    {
        _dungeonMaster.Player.ActionRow.Add(_dungeonMaster.Player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_dungeonMaster.Player.ActionRowPanel);
        _dungeonMaster.Player.Belt.Remove(_dungeonMaster.Player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }

    private void FromRowToBelt()
    {
        _dungeonMaster.Player.Belt.Add(_dungeonMaster.Player.ActionRow.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_dungeonMaster.Player.BeltPanel);
        _dungeonMaster.Player.ActionRow.Remove(_dungeonMaster.Player.ActionRow.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }
}