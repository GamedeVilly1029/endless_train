using TMPro;
using UnityEngine;

public class TurnCounterUI : MonoBehaviour
{
    [SerializeField] private TurnMaster _turnMaster;
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        _text.text = $"Turn: {_turnMaster.TurnNumber}";
    }
}
