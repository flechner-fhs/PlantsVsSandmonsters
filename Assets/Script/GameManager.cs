using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private bool MakeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return false;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);
        return true;
    }

    [Header("Audio Settings")]
    public float Volume;
    public AudioMixer AudioMixer;

    [Header("Player Data")]
    public int PlayerSave = 0;
    public int UnlockedLevels = 0;
    public string Playername = "";

    public int CurrentLevel = 0;

    private SaveManager SaveManager;

    private void Awake()
    {
        if (!MakeSingleton())
            return;

        SaveManager = GetComponent<SaveManager>();
        InitSoundVolume();
    }

    public List<string> GetSaveNames()
    {
        return SaveManager.GetSaveSlotNames(3).ToList();
    }

    public void LoadSave(int slot)
    {
        PlayerSave = slot;
        int? levels = SaveManager.ReadIntFromSave(slot, SaveManager.SaveDataKeys.UnlockedLevel);
        UnlockedLevels = levels ?? 1;

        string loadedName = SaveManager.ReadStringFromSave(slot, SaveManager.SaveDataKeys.Playername);

        if (loadedName == "" || loadedName == null)
            Playername = "Player " + slot;
        else
            Playername = loadedName;

        if (levels == null)
            StoreToSave(slot);
    }

    public void StoreToSave(int slot)
    {
        SaveManager.SaveIntToSave(slot, SaveManager.SaveDataKeys.UnlockedLevel, UnlockedLevels);
        SaveManager.SaveStringToSave(slot, SaveManager.SaveDataKeys.Playername, Playername);
    }

    public void ClearSave(int slot) => SaveManager.EraseSave(slot);

    public void StageCleared()
    {
        if (UnlockedLevels == CurrentLevel)
            UnlockedLevels++;
        StoreToSave(PlayerSave);
    }

    private void InitSoundVolume()
    {
        SetSoundVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
    }

    public void SetSoundVolume(float ratio)
    {
        Volume = ratio;
        float db = Mathf.Lerp(-40, 12, ratio);
        AudioMixer.SetFloat("BackgroundMusicVolume", db < -35 ? -80 : db);
        PlayerPrefs.SetFloat("MusicVolume", ratio);
    }
}
