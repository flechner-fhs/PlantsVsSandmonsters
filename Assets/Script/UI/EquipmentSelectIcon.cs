using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectIcon : MonoBehaviour
{
    public Equipment Equipment;

    public Image EquipImageDisplay;
    public Text CostText;

    public void Init(Equipment equipment, bool hideCost = false)
    {
        Equipment = equipment;
        EquipImageDisplay.sprite = equipment.Sprite;
        CostText.text = $"{equipment.Cost}";
        EquipImageDisplay.transform.localPosition = equipment.SpriteOffset;
        if (hideCost)
            CostText.transform.parent.gameObject.SetActive(false);
    }
}
