using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Screen References")]
    public GameObject CurrentScreen;
    public GameObject MainMenuScreen;

    [Header("Audio Mixer References")]
    public AudioMixer AudioMixer;
    public string ExposedParameter;

    [Header("Save State Display")]
    public List<Button> SaveSlotButtons;

    [Header("Main Menu Screen")]
    public Text MainMenuTitle;

    [Header("Equipment Display")]
    public GameObject EquipmentDisplay;
    public GameObject EquipmentIconPrefab;

    [Header("Level Selection Screen")]
    public List<Button> LevelButtons;

    [Header("Level References")]
    public List<string> LevelScenes;

    private void Start()
    {
        GameManager.Instance.LevelSelector = this;
        UpdateSlotButtons();
        UpdateLevelButtons();
        UpdateEquipmentDisplay();
    }

    public void UpdateEquipmentDisplay()
    {
        EquipmentDisplay.GetComponentsInChildren<EquipmentSelectIcon>().ToList().ForEach(x => Destroy(x.gameObject));
        GameManager.Instance.EquipmentManager.ActiveEquipments.ForEach(x => Instantiate(EquipmentIconPrefab, EquipmentDisplay.transform).GetComponent<EquipmentSelectIcon>().Init(x, true));
    }

    public void UpdateSlotButtons()
    {
        List<string> names = GameManager.Instance.GetSaveNames();

        for (int i = 0; i < names.Count; i++)
        {
            if (names[i] != "" && names[i] != null)
            {
                SaveSlotButtons[i].GetComponentInChildren<Text>().text = names[i];

                Button DeleteButton = SaveSlotButtons[i].transform.parent.GetComponentsInChildren<Button>(true).Where(x => x != SaveSlotButtons[i]).FirstOrDefault();
                if (DeleteButton)
                    DeleteButton.gameObject.SetActive(true);
            }
            else
            {
                SaveSlotButtons[i].GetComponentInChildren<Text>().text = "Empty";

                Button DeleteButton = SaveSlotButtons[i].transform.parent.GetComponentsInChildren<Button>(true).Where(x => x != SaveSlotButtons[i]).FirstOrDefault();
                if (DeleteButton)
                    DeleteButton.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateLevelButtons()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            if (i < GameManager.Instance.UnlockedLevels)
                LevelButtons[i].interactable = true;
            else
                LevelButtons[i].interactable = false;
        }
    }

    public void ChangeScreen(GameObject newScreen)
    {
        CurrentScreen.SetActive(false);
        newScreen.SetActive(true);
        CurrentScreen = newScreen;
    }

    public void OpenSavesWhenLogedOut(GameObject newScreen)
    {
        if (GameManager.Instance.PlayerSave == 0)
            ChangeScreen(newScreen);
        else
            ChangeScreen(MainMenuScreen);
    }
    public void LogOut(GameObject newScreen)
    {
        GameManager.Instance.StoreToSave();
        GameManager.Instance.PlayerSave = 0;
        ChangeScreen(newScreen);
    }

    public void LoadSave(int slot)
    {
        GameManager.Instance.LoadSave(slot);
        ChangeScreen(MainMenuScreen);
        MainMenuTitle.text = "Welcome " + GameManager.Instance.Playername;
        UpdateSlotButtons();
        UpdateLevelButtons();
    }

    public void ClearSave(int slot)
    {
        GameManager.Instance.ClearSave(slot);
        UpdateSlotButtons();
    }

    public void ChooseButton(int levelNumber)
    {
        int level = Mathf.Clamp(levelNumber - 1, 0, LevelScenes.Count - 1);
        GameManager.Instance.TransitionController.ChangeScene(LevelScenes[level]);
        //StartCoroutine(FadeOutMain.StartFade(AudioMixer, ExposedParameter, 0.5f, 0.01f, level, Scenes));

        GameManager.Instance.CurrentLevel = levelNumber;
    }

    public void StartButton()
    {
        ChooseButton(GameManager.Instance.UnlockedLevels);
    }
}
