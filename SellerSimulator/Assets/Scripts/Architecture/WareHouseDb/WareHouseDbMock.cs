using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouseDb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

class WareHouseDbMock : IWareHouseSource
{
    private static WareHouseDbMock instance;
    private static ulong currentMaxId = 0;
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

    //Лист в котором будет хранится купленные коробки
    private List<ModelBox> purchasedItems = new List<ModelBox>();
    public void AddPurchasedItem(ModelBox item)
    {

        item.id = ++currentMaxId;

        purchasedItems.Add(item);
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
