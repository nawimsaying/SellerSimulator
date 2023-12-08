using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;
using System;
using Assets.Scripts.Player;

public class OfflineItemSeller : MonoBehaviour
{
    public static event Action SalesCompleted;

    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    private PlayerData _playerData;


    void Start()
    {
        if (!CheckStatus.HasRun)
        {
            _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
            �alculationItemSaled();
        }
    }

    void �alculationItemSaled()
    {
        int resultMoney = 0;
        itemsToSell = _onSaleFrameRepository.GetAll();
        DateTime lastSaveTime = DateTimeManager.GetDayTime("LastSaveTime");
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int countSaleItem = 0;
        int countSaleItemForTime = 0;
        foreach (var item in itemsToSell)
        {
            _playerData = PlayerDataHolder.playerData;
            int chance = Convert.ToInt32(item.liquidity * 100 * item.buffLiquidity);
            //int chance = 100;
            int sellItemTime = 100 / chance;

            countSaleItemForTime = (int)timePassed.TotalSeconds / sellItemTime;

            if (countSaleItemForTime >= item.countProduct)
            {
                resultMoney = item.priceProduct * item.countProduct;
                _playerData.AddCoins(resultMoney);
                countSaleItem += item.countProduct;
                item.countProduct = 0;
            }
            else
            {
                resultMoney = item.priceProduct * countSaleItemForTime;
                _playerData.AddCoins(resultMoney);
                countSaleItem += countSaleItemForTime;
                item.countProduct -= countSaleItemForTime;
            }

        }

        Debug.Log($"������ ��������� {countSaleItem} �� {(int)timePassed.TotalSeconds} ������");
        
        _onSaleFrameRepository.SaveDataList(itemsToSell);
        // �������� ������� ����� ���������� ������
        SalesCompleted?.Invoke();
    }

    private void OnApplicationQuit()    
    {
        DateTimeManager.SetDayTime("LastSaveTime", DateTime.UtcNow);
    }
}
