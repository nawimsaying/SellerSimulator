using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseData : MonoBehaviour
{
    [SerializeField] private GameObject _buttonBox;

    [NonSerialized] public static int smallBoxes = 0;
    [NonSerialized] public static int bigBoxes = 0;

    private static GameObject _buttonBoxStatic;

    private WareHouseRepository _houseRepository;
    private List<ModelWareHouse> _wareHouseRepositories;

    private void Start()
    {
        _buttonBoxStatic = _buttonBox;

        _houseRepository = new WareHouseRepository(WareHouseDbMock.Instance);

        _wareHouseRepositories = _houseRepository.GetAll();

        Debug.Log("WH: " + _wareHouseRepositories);

        SortByBoxes();
    }

    private void SortByBoxes()
    {
        //_wareHouseRepositories.
    }
}
