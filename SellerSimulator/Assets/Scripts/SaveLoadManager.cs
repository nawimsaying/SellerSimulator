using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;
using Assets.Scripts.Architecture.WareHouseDb;
using Newtonsoft.Json;

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

            var res = JsonUtility.FromJson<T>(loadedString);

            return res;
        }
        else
            return new T();
    }

    public static void Save(string key, ToolBarList saveData)
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
                //jsonDataString += "[";
                jsonDataString += JsonUtility.ToJson(saveData.toolBarList[i], true);
                jsonDataString += "$";
            }

            //if (i == saveData.toolBarList.Count - 1)
            //    jsonDataString += "]";
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static ToolBarList Load(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                List<ModelWareHouse> res = new List<ModelWareHouse>() { };

                ModelWareHouse objectMy = new ModelWareHouse();

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    objectMy = JsonConvert.DeserializeObject<ModelWareHouse>(jsonArray[i]);
                    //objectMy = JsonUtility.FromJson<ModelWareHouse>(jsonArray[i]);
                    res.Add(objectMy);
                }
                ToolBarList toolBarList = new ToolBarList();
                toolBarList.toolBarList = res;

                return toolBarList;
            }
            else
                return new ToolBarList();
        }
        else
            return new ToolBarList();
    }
}