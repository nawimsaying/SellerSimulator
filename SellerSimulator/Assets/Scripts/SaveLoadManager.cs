using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;
using Assets.Scripts.Architecture.WareHouseDb;
using Newtonsoft.Json;

public static class SaveLoadManager
{
    public static void SaveToolBarList(string key, ToolBarList saveData)
    {
        string jsonDataString = null;

        for (int i = 0; i < saveData.toolBarList.Count; i++)
        {
            if (i == saveData.toolBarList.Count - 1)
            {
                jsonDataString += JsonUtility.ToJson(saveData.toolBarList[i], true);
            }
            else
            {
                jsonDataString += JsonUtility.ToJson(saveData.toolBarList[i], true);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveSampleList(string key, List<Sample> saveData)
    {
        string jsonDataString = null;

        for (int i = 0; i < saveData.Count; i++)
        {
            if (i == saveData.Count - 1)
            {
                jsonDataString += JsonUtility.ToJson(saveData[i], true);
            }
            else
            {
                jsonDataString += JsonUtility.ToJson(saveData[i], true);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveSamplesOnFramesList(string key, List<SamplesOnFrames> saveData)
    {
        string jsonDataString = null;

        for (int i = 0; i < saveData.Count; i++)
        {
            if (i == saveData.Count - 1)
            {
                jsonDataString += JsonUtility.ToJson(saveData[i], true);
            }
            else
            {
                jsonDataString += JsonUtility.ToJson(saveData[i], true);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static ToolBarList LoadToolBarList(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                List<ModelWareHouse> result = new List<ModelWareHouse>() { };

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    ModelWareHouse objectMy = JsonConvert.DeserializeObject<ModelWareHouse>(jsonArray[i]);
                    result.Add(objectMy);
                }
                ToolBarList toolBarList = new ToolBarList();
                toolBarList.toolBarList = result;

                return toolBarList;
            }
            else
                return new ToolBarList();
        }
        else
            return new ToolBarList();
    }

    public static List<Sample> LoadSampleList(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                List<Sample> result = new List<Sample>();

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    Sample objectMy = JsonConvert.DeserializeObject<Sample>(jsonArray[i]);
                    result.Add(objectMy);
                }
                return result;
            }
            else
                return new List<Sample>();
        }
        else
            return new List<Sample>();
    }

    public static List<SamplesOnFrames> LoadSamplesOnFramesList(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                List<SamplesOnFrames> result = new List<SamplesOnFrames>();

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    SamplesOnFrames objectMy = JsonConvert.DeserializeObject<SamplesOnFrames>(jsonArray[i]);
                    result.Add(objectMy);
                }
                return result;
            }
            else
                return new List<SamplesOnFrames>();
        }
        else
            return new List<SamplesOnFrames>();
    }
}