using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private DungeonMaster master;
    [SerializeField] private Transform MonsterTransform;
    [SerializeField] private RectTransform HPBar;
    [SerializeField] private TextMeshProUGUI HPBarText;
    public int HP;

    private void Start()
    {
        MonsterTransform.position = master.Cells[3].CellPosition;
        master.Cells[3].IsOcupiedByEntity = true;

        HPBar.position = Camera.main.WorldToScreenPoint(new Vector3(MonsterTransform.position.x, MonsterTransform.position.y + 1, 0));
        HP = 10;
    }

    private void Update()
    {
        HPBarText.text = HP.ToString();
    }
}