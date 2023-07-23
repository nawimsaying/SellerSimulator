using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouseDb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WareHouseDbMock : IWareHouseSource
{

    //Лист в котором будет хранится купленные коробки
    private List<ModelBox> purchasedItems = new List<ModelBox>();
    public void AddPurchasedItem(ModelBox item)
    {
        purchasedItems.Add(item);
        Debug.Log("tut");
    }
}
