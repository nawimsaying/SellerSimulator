using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.WareHouse;
using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeLiquidity : MonoBehaviour
{
    private int _timeUpdateLiquid = 1;

    private static ChangeLiquidity _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    void Update()
    {
        DateTime lastSaveTime = DateTimeManager.GetDayTimeForLiquid("LastSaveTimeForLiquid");
        TimeSpan timeDifference = DateTime.UtcNow - lastSaveTime;
        if(timeDifference.TotalMinutes > _timeUpdateLiquid)
        {
            ChangeLiquidTime();
        }
    }



    private void ChangeLiquidTime()
    {

        WareHouseDbMock wareHouseDbMock = SaveLoadManager.LoadWareHouseDbMockList();

        List<ModelBox> listWareHouse = wareHouseDbMock.purchasedItems;

        foreach (var item in listWareHouse)
        {
            item.idProduct.liquidity = 0;
        }

        foreach (ModelBox item in listWareHouse)
        {
            if (item.idProduct.liquidity == 0)
            {
                for (int i = 0; i < listWareHouse.Count; i++)
                {
                    if (listWareHouse[i].idProduct.id == item.idProduct.id)
                    {
                        if (listWareHouse[i].idProduct.liquidity != 0)
                        {
                            item.idProduct.liquidity = listWareHouse[i].idProduct.liquidity;
                            SaveLoadManager.SaveWareHouseDbMockList(wareHouseDbMock);
                            break;
                        }
                    }
                }

                if (item.idProduct.liquidity == 0)
                {
                    item.idProduct.liquidity = Random.Range(0.01f, 0.2f);
                    SaveLoadManager.SaveWareHouseDbMockList(wareHouseDbMock);
                }
            }
        }

        Debug.Log("Обновил Ликвидность предметов");

        DateTimeManager.SetDayTime("LastSaveTimeForLiquid", DateTime.UtcNow);
    }

    public void ChangeLiquidForBuy()
    {

        WareHouseDbMock wareHouseDbMock = SaveLoadManager.LoadWareHouseDbMockList();

        List<ModelBox> listWareHouse = wareHouseDbMock.purchasedItems;

        foreach (ModelBox item in listWareHouse)
        {
            if (item.idProduct.liquidity == 0)
            {
                for (int i = 0; i < listWareHouse.Count; i++)
                {
                    if (listWareHouse[i].idProduct.id == item.idProduct.id)
                    {
                        if (listWareHouse[i].idProduct.liquidity != 0)
                        {
                            item.idProduct.liquidity = listWareHouse[i].idProduct.liquidity;
                            SaveLoadManager.SaveWareHouseDbMockList(wareHouseDbMock);
                            break; 
                        }
                    }
                }

                if (item.idProduct.liquidity == 0)
                {
                    item.idProduct.liquidity = Random.Range(0.01f, 0.2f);
                    SaveLoadManager.SaveWareHouseDbMockList(wareHouseDbMock);
                }
            }
        }





    }

}
