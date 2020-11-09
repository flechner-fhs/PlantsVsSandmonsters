using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class LevelSelector : MonoBehaviour
{

    public GameObject CurrentScreen;
    public GameObject MainMenuScene;

    public AudioMixer AudioMixer;
    public string ExposedParameter;

    public Text MainMenuTitle;

    public List<Button> SaveSlotButtons;

    public List<Button> LevelButtons;

    public List<string> Scenes;

    private void Start()
    {
        UpdateSlotButtons();
        UpdateLevelButtons();
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
                if(DeleteButton)
                    DeleteButton.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateLevelButtons()
    {
        for(int i = 0; i < LevelButtons.Count; i++)
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

    public void LoadSave(int slot)
    {
        GameManager.Instance.LoadSave(slot);
        ChangeScreen(MainMenuScene);
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
        int level = Mathf.Clamp(levelNumber - 1, 0, Scenes.Count - 1);
        StartCoroutine(FadeOutMain.StartFade(AudioMixer, ExposedParameter, 0.5f, 0.01f, level, Scenes));

        GameManager.Instance.CurrentLevel = levelNumber;
    }

    public void StartButton()
    {
        ChooseButton(1);
    }
}
