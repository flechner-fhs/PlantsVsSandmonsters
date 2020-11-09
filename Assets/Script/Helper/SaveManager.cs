using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public enum SaveDataKeys{
        Playername,
        UnlockedLevel
    }

    public string[] GetSaveSlotNames(int slotAmount)
    {
        string[] slots = new string[slotAmount];

        for(int i = 0; i < slotAmount; i++)
        {
            slots[i] = ReadStringFromSave(i+1, SaveDataKeys.Playername);
        }

        return slots;
    }

    public void EraseAllSaves() => PlayerPrefs.DeleteAll();

    public void EraseSave(int slot)
    {
        foreach (SaveDataKeys s in Enum.GetValues(typeof(SaveDataKeys)))
            PlayerPrefs.DeleteKey("Save" + slot + ":" + s);
    }

    public string ReadStringFromSave(int save, SaveDataKeys key) => PlayerPrefs.GetString("Save" + save + ":" + key, "");
    public int? ReadIntFromSave(int save, SaveDataKeys key)
    {
        int? val = PlayerPrefs.GetInt("Save" + save + ":" + key, int.MinValue);
        return val == int.MinValue ? null : val;
    }
    public float? ReadFloatFromSave(int save, SaveDataKeys key)
    {
        float? val = PlayerPrefs.GetFloat("Save" + save + ":" + key, float.NaN);
        return val == float.NaN ? null : val;
    }

    public void SaveStringToSave(int save, SaveDataKeys key, string value) => PlayerPrefs.SetString("Save" + save + ":" + key, value);
    public void SaveIntToSave(int save, SaveDataKeys key, int value) => PlayerPrefs.SetInt("Save" + save + ":" + key, value);
    public void SaveFloatToSave(int save, SaveDataKeys key, float value) => PlayerPrefs.SetFloat("Save" + save + ":" + key, value);
}
