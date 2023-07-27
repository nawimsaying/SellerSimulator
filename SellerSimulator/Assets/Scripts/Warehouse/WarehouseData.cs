using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseData : MonoBehaviour
{
    [SerializeField] private GameObject _buttonBox;
    [SerializeField] private Text _textSmallBox;

    [NonSerialized] public static int smallBoxes = 0;
    [NonSerialized] public static int bigBoxes = 0;

    private static GameObject _buttonBoxStatic;

    private WareHouseRepository _houseRepository;
    private List<ModelWareHouse> _wareHouseRepositories;

    private void Start()
    {
        _buttonBoxStatic = _buttonBox;
        
        // Receive data from the warehouse in the computer 
        _houseRepository = new WareHouseRepository(WareHouseDbMock.Instance);
        _wareHouseRepositories = _houseRepository.GetAll();

        // Search new boxes
        List<ModelWareHouse> newBoxesList = SearchNewBoxes(_wareHouseRepositories);
        // Load old boxes in tool bar
        List<ModelWareHouse> oldToolBarData = SaveLoadManager.LoadData<List<ModelWareHouse>>("toolBarList");
        // Concat old boxes with new boxes
        List<ModelWareHouse> newToolBarData = oldToolBarData.Concat(newBoxesList).ToList();
        // Save new boxes in tool bar
        SaveLoadManager.SaveData("toolBarList", newToolBarData);

        SortBoxesBySize(newToolBarData);
    }

    private void FixedUpdate()
    {
        _textSmallBox.text = Convert.ToString(PlayerPrefs.GetInt("smallBoxes"));
    }

    private void SortBoxesBySize(List<ModelWareHouse> toolBarList)
    {
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

    private List<ModelWareHouse> SearchNewBoxes(List<ModelWareHouse> allData)
    {
        var oldSetedData = SaveLoadManager.LoadData<List<ModelWareHouse>>("setedList");
        var oldToolBarData = SaveLoadManager.LoadData<List<ModelWareHouse>>("toolBarList");

        if (oldSetedData.Count > 0)
        {
            for (int i = 0; i < allData.Count; i++)
            {
                for (int j = 0; j < oldSetedData.Count; i++)
                {
                    if (allData[i].idBox == oldSetedData[j].idBox)
                    {
                        allData.Remove(allData[i]);
                        break;
                    }
                }
            }
        }

        if (oldToolBarData.Count > 0)
        {
            for (int i = 0; i < allData.Count; i++)
            {
                for (int j = 0; j < oldToolBarData.Count; j++)
                {
                    if (allData[i].idBox == oldToolBarData[j].idBox)
                    {
                        allData.Remove(allData[i]);
                        break;
                    }
                }
            }
        }

        return allData;
    }
}
