using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeltUIController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private DungeonMaster _dungeonMaster;
    [SerializeField] private ActionRow _playerActionRow;

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
                FindFirstObjectByType<AudioMaster>().PlaySound("equipAction");
            }
            else if (_objectWasClicked.transform.parent == _playerActionRow.Panel)
            {
                FromRowToBelt();
                _playerActionRow.OnActionRemove.Invoke();
                FindFirstObjectByType<AudioMaster>().PlaySound("unequipAction");
            }
        }
    }

    private void FromBeltToRow()
    {
        _playerActionRow.Actions.Add(_dungeonMaster.Player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_playerActionRow.Panel);
        _dungeonMaster.Player.Belt.Remove(_dungeonMaster.Player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }

    private void FromRowToBelt()
    {
        _dungeonMaster.Player.Belt.Add(_playerActionRow.Actions.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_dungeonMaster.Player.BeltPanel);
        _playerActionRow.Actions.Remove(_playerActionRow.Actions.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }
}