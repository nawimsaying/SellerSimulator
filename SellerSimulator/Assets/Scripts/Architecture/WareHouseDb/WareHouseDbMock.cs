using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WareHouseDbMock : IWareHouseSource
{
    private WareHouseDbMock _dataList;
    private ulong currentMaxId = 0;    

    //���� � ������� ����� �������� ��������� �������
     public List<ModelBox> purchasedItems = new List<ModelBox>();


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

        WareHouseDbMock data = SaveLoadManager.LoadWareHouseDbMockList(); // record
        purchasedItems = data.purchasedItems; //duplicate list

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
        data.purchasedItems = purchasedItems; //copy list in data
        SaveLoadManager.SaveWareHouseDbMockList(data); //save data(list)
    }

    Result<List<ModelWareHouse>> IWareHouseSource.GetAll()
    {


        List<ModelWareHouse> result = new List<ModelWareHouse>();

        _dataList = SaveLoadManager.LoadWareHouseDbMockList();

        foreach (var wareHouseBox in _dataList.purchasedItems)
        {
            ModelWareHouse modelWareHouse = new ModelWareHouse()
            {
                idBox = wareHouseBox.id,
                idProduct = wareHouseBox.idProduct.id,
                sizeBox = wareHouseBox.sizeBox,
                productCount = wareHouseBox.countProduct,
                productName = wareHouseBox.idProduct.name,
                liquidity = wareHouseBox.idProduct.liquidity
            };

            result.Add(modelWareHouse);
        }

        return Result<List<ModelWareHouse>>.Success(result);
    }
}
