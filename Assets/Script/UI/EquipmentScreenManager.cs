using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentScreenManager : MonoBehaviour
{
    [Header("References")]
    public GameObject LevelSelectScreen;
    public GameObject SelectedEquipmentContainer;
    public GameObject EquipmentContainer;
    public GameObject SpeechBubble;
    public Text PointTracker;
    public Button ConfirmButton;

    [Header("Prefabs")]
    public GameObject SelectedEquipPrefab;
    public GameObject EquipButtonPrefab;

    private int equipmentPoints = 0;

    private void Start()
    {
        GameManager.Instance.EquipmentManager.Equipments.ForEach(x =>
        {
            GameObject equipGo = Instantiate(EquipButtonPrefab, EquipmentContainer.transform);
            equipGo.GetComponent<EquipmentSlot>().Init(x);
        });
        GameManager.Instance.EquipmentManager.ActiveEquipments.ForEach(x => EquipClicked(x));
        UpdatePointTracker();
        HoverEnd();
    }

    public void HideSpeech() => SpeechBubble.SetActive(false);
    public void ShowSpeech(string text)
    {
        SpeechBubble.GetComponentInChildren<Text>().text = text;
        SpeechBubble.SetActive(true);
    }

    public void EquipClicked(Equipment equip)
    {
        if (SelectedEquipmentContainer.GetComponentsInChildren<EquipmentSelectIcon>().Where(x => x.Equipment == equip).Count() > 0)
        {
            SelectedEquipmentContainer.GetComponentsInChildren<EquipmentSelectIcon>().Where(x => x.Equipment == equip).ToList().ForEach(x => Destroy(x.gameObject));
            EquipmentContainer.GetComponentsInChildren<EquipmentSlot>().Where(x => x.Equipment == equip).FirstOrDefault().SetSelected(false);
            equipmentPoints -= equip.Cost;
            HoverEnd();
        }
        else
        {
            if (SelectedEquipmentContainer.GetComponentsInChildren<EquipmentSelectIcon>().Count() < GameManager.Instance.EquipmentManager.MaxEquipments)
            {
                EquipmentContainer.GetComponentsInChildren<EquipmentSlot>().Where(x => x.Equipment == equip).FirstOrDefault().SetSelected(true);
                GameObject selectedEquipGo = Instantiate(SelectedEquipPrefab, SelectedEquipmentContainer.transform);
                selectedEquipGo.GetComponent<EquipmentSelectIcon>().Init(equip);
                equipmentPoints += equip.Cost;
                HoverEnter(equip);
            }
            //else Fail-Animation
        }
        UpdatePointTracker();
    }

    public void Confirm()
    {
        List<Equipment> equips = SelectedEquipmentContainer.GetComponentsInChildren<EquipmentSelectIcon>().Select(x => x.Equipment).ToList();
        int total = equips.Select(x => x.Cost).Sum();
        int max = GameManager.Instance.EquipmentManager.MaxEquipmentPoints;

        if (total <= max)
        {
            HoverEnd();
            GameManager.Instance.EquipmentManager.ActiveEquipments = equips;
            GameManager.Instance.LevelSelector.UpdateEquipmentDisplay();
            GameManager.Instance.LevelSelector.ChangeScreen(LevelSelectScreen);
        }

    }

    public void UpdatePointTracker()
    {
        int total = equipmentPoints;
        int max = GameManager.Instance.EquipmentManager.MaxEquipmentPoints;

        PointTracker.text = $"{total} / {max}";
        if (total > max)
        {
            PointTracker.color = Color.red;
            ConfirmButton.interactable = false;
        }
        else
        {
            PointTracker.color = Color.white;
            ConfirmButton.interactable = true;
        }
    }

    public void HoverEnter(Equipment equip)
    {
        SpeechBubble.SetActive(true);
        SpeechBubble.GetComponentInChildren<Text>().text = equip.Description;
    }

    public void HoverEnd() => SpeechBubble.SetActive(false);
}
