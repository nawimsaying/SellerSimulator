using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouseDb;
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

    //Лист в котором будет хранится купленные коробки
    private List<ModelBox> purchasedItems = new List<ModelBox>();


    public void AddPurchasedItem(ModelBox item)
    {
        currentMaxId++; // Увеличиваем текущий максимальный айдишник на 1

        // Создаем новый объект ModelBox и копируем данные из переданного объекта item
        // Затем присваиваем новому товару текущее значение максимального айдишника
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
