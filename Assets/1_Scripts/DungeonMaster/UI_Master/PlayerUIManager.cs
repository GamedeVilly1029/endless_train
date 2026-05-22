using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUIManager : MonoBehaviour
{
    public List<RectTransform> PlayerUIObjects;
    [SerializeField] private UIMaster _uIMaster;

    private void Awake()
    {
        _uIMaster.NewFightUIStarterInst.OnStartFightButtonClick.AddListener(TurnUIOn);
    }

    public void TurnUIOff()
    {
        foreach (RectTransform obj in PlayerUIObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void TurnUIOn()
    {
        foreach (RectTransform obj in PlayerUIObjects)
        {
            obj.gameObject.SetActive(true);
        }
    }
}