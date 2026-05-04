using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeltUIController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerActor _player;
    [SerializeField] private TurnProcessor _turnProcessor;
    [SerializeField] private LevelMaster _levelMaster;
    [SerializeField] private ActionRow _playerActionRow;

    private GameObject _objectWasClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("UIActionIcon"))
        {
            _objectWasClicked = eventData.pointerCurrentRaycast.gameObject;
            if (_objectWasClicked.transform.parent == _player.BeltPanel)
            {
                if (_player.ActionInRowCount == _player.ActionInRowLimit)
                {
                    Debug.Log("At action number limit");
                }
                else
                {
                    FromBeltToRow();
                    _playerActionRow.OnActionAdd.Invoke();
                    FindFirstObjectByType<AudioMaster>().PlaySound("equipAction");
                    _player.ActionInRowCount += 1;
                }
            }
            else if (_objectWasClicked.transform.parent == _playerActionRow.Panel)
            {
                FromRowToBelt();
                _playerActionRow.OnActionRemove.Invoke();
                FindFirstObjectByType<AudioMaster>().PlaySound("unequipAction");
                _player.ActionInRowCount -= 1;
            }
        }
    }

    private void FromBeltToRow()
    {
        _playerActionRow.Actions.Add(_player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_playerActionRow.Panel);
        _levelMaster.Player.Belt.Remove(_player.Belt.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }

    private void FromRowToBelt()
    {
        _player.Belt.Add(_playerActionRow.Actions.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
        _objectWasClicked.transform.SetParent(_player.BeltPanel);
        _playerActionRow.Actions.Remove(_playerActionRow.Actions.FirstOrDefault(x => x.UIRepresentation == _objectWasClicked));
    }
}