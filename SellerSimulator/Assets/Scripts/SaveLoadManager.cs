using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;
using Assets.Scripts.Architecture.WareHouseDb;
using Newtonsoft.Json;
using Assets.Scripts.Architecture.MainDb;
using Unity.VisualScripting;
using Assets.Scripts.Architecture.MainDb.ModelsDb;

public static class SaveLoadManager
{
    public static void SaveToolBarList(ToolBarList saveData)
    {
        string key = "toolBarList";

        string jsonDataString = null;

        for (int i = 0; i < saveData.toolBarList.Count; i++)
        {
            if (i == saveData.toolBarList.Count - 1)
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.toolBarList[i]);
            }
            else
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.toolBarList[i]);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveSampleList(List<Sample> saveData)
    {
        string key = "sampleList";

        string jsonDataString = null;

        for (int i = 0; i < saveData.Count; i++)
        {
            if (i == saveData.Count - 1)
            {
                jsonDataString += JsonConvert.SerializeObject(saveData[i]);
            }
            else
            {
                jsonDataString += JsonConvert.SerializeObject(saveData[i]);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveSamplesOnFramesList(List<SamplesOnFrames> saveData)
    {
        string key = "samplesOnFrameList";
        string jsonDataString = null;

        for (int i = 0; i < saveData.Count; i++)
        {
            if (i == saveData.Count - 1)
            {
                jsonDataString += JsonConvert.SerializeObject(saveData[i]);
            }
            else
            {
                jsonDataString += JsonConvert.SerializeObject(saveData[i]);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveMainDbMockList(MainDbMock saveData)
    {
        string key = "mainDbMockList";
        string jsonDataString = null;

        for (int i = 0; i < saveData.ListBox.Count; i++)
        {
            if (i == saveData.ListBox.Count - 1)
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.ListBox[i]);
            }
            else
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.ListBox[i]);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static void SaveWareHouseDbMockList(WareHouseDbMock saveData)
    {
        string key = "wareHouseDbMockList";
        string jsonDataString = null;

        for (int i = 0; i < saveData.purchasedItems.Count; i++)
        {
            if (i == saveData.purchasedItems.Count - 1)
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.purchasedItems[i]);
            }
            else
            {
                jsonDataString += JsonConvert.SerializeObject(saveData.purchasedItems[i]);
                jsonDataString += "$";
            }
        }
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static ToolBarList LoadToolBarList()
    {
        string key = "toolBarList";

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

    public static List<Sample> LoadSampleList()
    {
        string key = "sampleList";

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

    public static List<SamplesOnFrames> LoadSamplesOnFramesList()
    {
        string key = "samplesOnFrameList";

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

    public static MainDbMock LoadMainDbMockList()
    {
        string key = "mainDbMockList";

        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                MainDbMock result = new MainDbMock();
                List<ModelBox> modelBoxList = new List<ModelBox>();

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    ModelBox objectMy = JsonConvert.DeserializeObject<ModelBox>(jsonArray[i]);

                    modelBoxList.Add(objectMy);

                    result.ListBox = modelBoxList;
                }
                return result;
            }
            else
                return new MainDbMock();
        }
        else
            return new MainDbMock();
    }

    public static WareHouseDbMock LoadWareHouseDbMockList()
    {
        string key = "wareHouseDbMockList";

        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);

            if (loadedString != "")
            {
                string[] jsonArray = loadedString.Split("$");

                WareHouseDbMock result = new WareHouseDbMock();
                List<ModelBox> modelBoxList = new List<ModelBox>();

                for (int i = 0; i < jsonArray.Length; i++)
                {
                    ModelBox objectMy = JsonConvert.DeserializeObject<ModelBox>(jsonArray[i]);

                    modelBoxList.Add(objectMy);

                    result.purchasedItems = modelBoxList;
                }
                return result;
            }
            else
                return new WareHouseDbMock();
        }
        else
            return new WareHouseDbMock();
    }
}