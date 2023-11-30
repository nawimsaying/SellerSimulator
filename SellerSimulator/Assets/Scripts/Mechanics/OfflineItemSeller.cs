using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;
using System;

public class OfflineItemSeller : MonoBehaviour
{
    public static event Action SalesCompleted;

    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    

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
        itemsToSell = _onSaleFrameRepository.GetAll();
        DateTime lastSaveTime = DateTimeManager.GetDayTime("LastSaveTime");
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int countSaleItem = 0;
        foreach (var item in itemsToSell)
        {
            int chance = Convert.ToInt32(item.liquidity * 100);
            int sellItemTime = 100 / chance;

            countSaleItem = (int)timePassed.TotalSeconds / sellItemTime;

            if (countSaleItem >= item.countProduct)
            {
                item.countProduct = 0;
            }
            else
            {
                item.countProduct -= countSaleItem;
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
