using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

class WareHouseDbMock : IWareHouseSource
{
    private static WareHouseDbMock instance;
    private ulong currentMaxId = 0;

    public static WareHouseDbMock Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WareHouseDbMock();
            }
            return instance;
        }
    }

    //���� � ������� ����� �������� ��������� �������
    private List<ModelBox> purchasedItems = new List<ModelBox>();


    public void AddPurchasedItem(ModelBox item)
    {
        if (PlayerPrefs.HasKey("CurrentMaxId"))
        {
            string savedValue = PlayerPrefs.GetString("CurrentMaxId");
            if (ulong.TryParse(savedValue, out ulong loadedValue))
            {
                currentMaxId = loadedValue;
            }
        }

        currentMaxId++; // ����������� ������� ������������ �������� �� 1

        PlayerPrefs.SetString("CurrentMaxId", currentMaxId.ToString());
        PlayerPrefs.Save();

        // ������� ����� ������ ModelBox � �������� ������ �� ����������� ������� item
        // ����� ����������� ������ ������ ������� �������� ������������� ���������
        ModelBox newItem = new ModelBox()
        {
            id = currentMaxId,
            nameBox = item.nameBox,
            imageBox = item.imageBox,
            price = item.price,
            countBox = item.countBox,
            countProduct = item.countProduct,
            sizeBox = item.sizeBox,
            idProduct = item.idProduct,
        };

        purchasedItems.Add(newItem);
        Debug.Log("tut");
    }

    Result<List<ModelWareHouse>> IWareHouseSource.GetAll()
    {
        List<ModelWareHouse> result = new List<ModelWareHouse>();

        foreach (var wareHouseBox in purchasedItems)
        {
            ModelWareHouse modelWareHouse = new ModelWareHouse()
            {
                idBox = wareHouseBox.id,
                idProduct = wareHouseBox.idProduct.id,
                sizeBox = wareHouseBox.sizeBox,
                productCount = wareHouseBox.countProduct,
                productName = wareHouseBox.idProduct.name,
            };

            result.Add(modelWareHouse);
        }

        return Result<List<ModelWareHouse>>.Success(result);
    }
}
