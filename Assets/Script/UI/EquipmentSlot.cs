using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment Equipment;

    public Image BackgroundDisplay;
    public Image EquipImageDisplay;
    public Text CostText;

    public Color HighlightColor = new Color(1, 0.95f, 0.33f, 1);

    private EquipmentScreenManager EquipmentScreenManager;

    public void Init(Equipment equipment)
    {
        Equipment = equipment;
        EquipImageDisplay.sprite = equipment.Sprite;
        CostText.text = $"{equipment.Cost}";
        EquipmentScreenManager = GetComponentInParent<EquipmentScreenManager>();
        SetSelected(false);
        GetComponentInChildren<EquipUIColider>().Init(equipment, EquipmentScreenManager);

        EquipImageDisplay.transform.localPosition = equipment.SpriteOffset;
    }

    public void SetSelected(bool val)
    {
        if (val)
            BackgroundDisplay.color = HighlightColor;
        else
            BackgroundDisplay.color = Color.white;
    }

    public void OnClicked()
    {
        EquipmentScreenManager.EquipClicked(Equipment);
    }
}
