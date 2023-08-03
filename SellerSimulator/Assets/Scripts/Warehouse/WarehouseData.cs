using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Palmmedia.ReportGenerator.Core.Common;

public class WarehouseData : MonoBehaviour
{
    [SerializeField] private GameObject _buttonBox;
    [SerializeField] private Text _textSmallBox;

    private static GameObject _buttonBoxStatic;

    private WareHouseRepository _houseRepository;
    private List<ModelWareHouse> _wareHouseRepositories;

    private void Start()
    {
        _buttonBoxStatic = _buttonBox;
        
        // Receive data from the warehouse in the computer 
        _houseRepository = new WareHouseRepository(WareHouseDbMock.Instance);
        _wareHouseRepositories = _houseRepository.GetAll();

        // DELETE
        //PlayerPrefs.DeleteAll();

        // Search sold and new boxes
        List<ModelWareHouse> newBoxesList = SearchNewBoxes(_wareHouseRepositories);

        // Load old boxes in tool bar
        ToolBarList oldToolBarData = SaveLoadManager.LoadToolBarList("toolBarList");

        // Concat old boxes with new boxes
        List<ModelWareHouse> newToolBarData;
        if (oldToolBarData.toolBarList != null)
            newToolBarData = oldToolBarData.toolBarList.Concat(newBoxesList).ToList();
        else
            newToolBarData = newBoxesList;

        // Save new boxes in tool bar
        SaveLoadManager.SaveToolBarList("toolBarList", GetSaveSnapshotToolBarList(newToolBarData));

        SortBoxesBySize(newToolBarData);
        Debug.Log("Ended!");
    }

    private void FixedUpdate()
    {
        int count = PlayerPrefs.GetInt("smallBoxes");
        _textSmallBox.text = Convert.ToString(PlayerPrefs.GetInt("smallBoxes"));
    }

    private void SortBoxesBySize(List<ModelWareHouse> toolBarList)
    {
        int smallBoxes = 0, bigBoxes= 0;

        // Sorting
        for (int i = 0; i < toolBarList.Count; i++)
        {
            if (toolBarList[i].sizeBox == "Small")
                smallBoxes++;
            else if (toolBarList[i].sizeBox == "Big")
                bigBoxes++;
            else
                Debug.LogError("Data leak in WarehouseData class, SortByBoxes method. Failed to distribute the box by size.");
        }

        // Save count of boxes
        PlayerPrefs.SetInt("smallBoxes", smallBoxes);
        PlayerPrefs.SetInt("bigBoxes", bigBoxes);

        // Show boxes in tool bar
        WarehouseButtons warehouseButtons = new WarehouseButtons();
        warehouseButtons.SpawnBoxesInToolBar();
    }

    private List<ModelWareHouse> SearchNewBoxes(List<ModelWareHouse> newData)
    {
        List<Sample> oldSampleList = SaveLoadManager.LoadSampleList("sampleList");
        ToolBarList oldToolBarData = SaveLoadManager.LoadToolBarList("toolBarList");
        
        if (oldSampleList.Count > 0)
        {
            for (int i = 0; i < oldSampleList.Count; i++)
            {
                for (int j = 0; j < oldSampleList[i].rackSample.Length; j++)
                {
                    for (int k = 0; k < newData.Count; k++)
                    {
                        if (newData[k].idBox == oldSampleList[i].rackSample[j])
                        {
                            newData.Remove(newData[k]);
                            break;
                        }
                    }
                }
            }
        }

        if (oldToolBarData.toolBarList != null)
        {
            for (int i = 0; i < oldToolBarData.toolBarList.Count; i++)
            {
                for (int j = 0; j < newData.Count; j++)
                {
                    if (newData[j].idBox == oldToolBarData.toolBarList[i].idBox)
                    {
                        newData.Remove(newData[j]);
                        break;
                    }
                }
            }
        }

        return newData;
    }

    public ToolBarList GetSaveSnapshotToolBarList(List<ModelWareHouse> _toolBarList)
    {
        var data = new ToolBarList()
        {
            toolBarList = _toolBarList
        };

        return data;
    }

    public void CalculateSoldItems(List<ModelWareHouse> newData)
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList("sampleList");
        List<ulong> indexes = new List<ulong>();

        for (int i = 0; i < sampleList.Count; i++)
        {
            for (int j = 0; j < sampleList[i].rackSample.Length; j++)
            {
                if (sampleList[i].rackSample[j] != 0)
                    indexes.Add(sampleList[i].rackSample[j]);
            }
        }

        for (int i = 0; i < indexes.Count; i++)
        {
            bool isSold = true;

            for (int j = 0; j < newData.Count; j++)
            {
                if (indexes[i] == newData[j].idBox)
                {
                    isSold = false;
                    break;
                }
            }

            if (isSold)
            {
                // Delete sold items from PlayerPrefs
                for (int j = 0; j < sampleList.Count; j++)
                {
                    for (int k = 0; k < sampleList[j].rackSample.Length; k++)
                    {
                        if (sampleList[j].rackSample[k] == indexes[i])
                        {
                            sampleList[j].rackSample[k] = 0;

                            SaveLoadManager.SaveSampleList("sampleList", sampleList);
                        }
                    }
                }
            }
        }
    }
}
