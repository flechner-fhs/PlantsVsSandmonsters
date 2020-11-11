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

    [Header("Player Data")]
    public int PlayerSave = 0;
    public int UnlockedLevels = 0;
    public string Playername = "";

    public int CurrentLevel = 0;

    [HideInInspector]
    public BackgroundMusicController MusicController;
    [HideInInspector]
    public TransitionController TransitionController;
    private SaveManager SaveManager;

    private void Awake()
    {
        if (!MakeSingleton())
            return;

        SaveManager = GetComponent<SaveManager>();
        MusicController = GetComponent<BackgroundMusicController>();
        TransitionController = GetComponent<TransitionController>();
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

    public void StoreToSave() => StoreToSave(PlayerSave);
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
}
