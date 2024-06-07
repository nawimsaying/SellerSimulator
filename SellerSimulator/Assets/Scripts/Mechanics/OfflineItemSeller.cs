using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;
using System;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine.UI;

public class OfflineItemSeller : MonoBehaviour
{
    public static event Action SalesCompleted;

    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    private PlayerData _playerData;

    [SerializeField] private GameObject _popWindow;
    [SerializeField] private GameObject _background;
    private TimeSpan _timePassed = TimeSpan.Zero;
    private int _coinPlayerForSell = 0;
    private int _expPlayerForSell = 0;


    void Start()
    {
        _popWindow.SetActive(false);
        _background.SetActive(false);
        if (!CheckStatus.HasRun)
        {
            _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
            CalculationItemSaled();
        }
    }

    void CalculationItemSaled()
    {
        int resultMoney = 0;
        itemsToSell = _onSaleFrameRepository.GetAll();
        DateTime lastSaveTime = DateTimeManager.GetDayTime("LastSaveTime");
        _timePassed = DateTime.UtcNow - lastSaveTime;
        int countSaleItem = 0;
        int countSaleItemForTime = 0;
        
        foreach (var item in itemsToSell)
        {
            _playerData = PlayerDataHolder.playerData;
            int chance = Convert.ToInt32(item.liquidity * 100 * item.buffLiquidity);
            //int chance = 100;
            int sellItemTime = (100 / chance) * 20;

            countSaleItemForTime = (int)_timePassed.TotalSeconds / sellItemTime;
            
            if (countSaleItemForTime >= item.countProduct)
            {
                resultMoney = item.priceProduct * item.countProduct;
                _coinPlayerForSell += resultMoney;
                int expForSale = item.countProduct * 32;
                _expPlayerForSell += expForSale;
                countSaleItem += item.countProduct;
                item.countProduct = 0;
            }
            else
            {
                resultMoney = item.priceProduct * countSaleItemForTime;
                _coinPlayerForSell += resultMoney;
                int expForSale = countSaleItemForTime * 32;
                _expPlayerForSell += expForSale;
                countSaleItem += countSaleItemForTime;
                item.countProduct -= countSaleItemForTime;
            }

        }

           DisplayPopInfo();




        Debug.Log($"������ ��������� {countSaleItem} �� {(int)_timePassed.TotalSeconds} ������");
        
        _onSaleFrameRepository.SaveDataList(itemsToSell);

        InfoBoxCells.BarBoxUpdate();
        // �������� ������� ����� ���������� ������
        SalesCompleted?.Invoke();
    }


    void DisplayPopInfo()
    {
      
        _popWindow.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _coinPlayerForSell.ToString();
        _popWindow.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = $"<color=\"black\">Время с прошлого визита:</color> {_timePassed.Days}d {_timePassed.Hours}h {_timePassed.Minutes}m {_timePassed.Seconds}s";
        Button buttonSell = _popWindow.transform.GetChild(2).GetComponent<Button>();
        buttonSell.onClick.RemoveAllListeners(); // ������� ��� ���������� �����������
        buttonSell.onClick.AddListener(() => ButtonContinueClicked());
        _popWindow.SetActive(true);
        _background.SetActive(true);

    }

    void ButtonContinueClicked()
    {
        Debug.Log("Click");
        _popWindow.SetActive(false);
        _background.SetActive(false);

       _playerData.AddCoins(_coinPlayerForSell);
        _playerData.AddExperience(_expPlayerForSell);
    }





    private void OnApplicationQuit()    
    {
        DateTimeManager.SetDayTime("LastSaveTime", DateTime.UtcNow);
    }
}
