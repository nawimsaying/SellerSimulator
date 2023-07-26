using Assets.Scripts.Architecture.WareHouseDb;
using System;
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

        SortBoxesBySize();

        WarehouseButtons.SpawnBoxesInToolBar();

        _textSmallBox.text = Convert.ToString(smallBoxes);
    }

    private void SortBoxesBySize()
    {
        for (int i = 0; i < _wareHouseRepositories.Count; i++)
        {
            if (_wareHouseRepositories[i].sizeBox == "Small")
                smallBoxes++;
            else if (_wareHouseRepositories[i].sizeBox == "Big")
                bigBoxes++;
            else
                Debug.LogError("Data leak in WarehouseData class, SortByBoxes method. Failed to distribute the box by size.");
        }
    }
}
