using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveData<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);

        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T LoadData<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            return JsonUtility.FromJson<T>(loadedString);
        }
        else
            return new T();
    }
}