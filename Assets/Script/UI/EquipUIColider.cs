using UnityEngine;

public class EquipUIColider : MonoBehaviour
{
    [HideInInspector]
    public Equipment Equipment;
    [HideInInspector]
    public EquipmentScreenManager EquipmentScreenManager;
    public void Init(Equipment equipment, EquipmentScreenManager equipmentScreenManager)
    {
        Equipment = equipment;
        EquipmentScreenManager = equipmentScreenManager;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        EquipmentScreenManager.HoverEnter(Equipment);
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
        EquipmentScreenManager.HoverEnd();
    }
}
