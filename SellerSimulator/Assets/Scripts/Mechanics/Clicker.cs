using Assets.Scripts.Architecture.OnSaleFrame;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Clicker : MonoBehaviour
{
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _canvasClickerMode;
    [SerializeField] private TextMeshProUGUI _clickerCounterText;
    private PlayerData _playerData;
    private bool _listNull;
    private int _successThreshold = 1;
    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    [NonSerialized] public static bool isClickerModeEnable = false;

    private int _clickCount = 0;

    private void Update()
    {
        if (isClickerModeEnable)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                _clickCount++;
                SellItems();
                _clickerCounterText.text = _clickCount.ToString();
            }
#else
            if (Input.touchCount < 4)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        _clickCount++;
                        SellItems();
                    }
                }
                //_text.text = _clickCount.ToString();
            }
#endif
        }
    }

    public void ChangeToggleValue()
    {
        if (!isClickerModeEnable)
            ToggleOn();
        else
            ToggleOff();
    }

    // Logic when the player has enabled clicker mode
    private void ToggleOn()
    {
        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        isClickerModeEnable = true;
        CheckStatus.ClickerHasRun = true;
        _canvasMain.SetActive(false);
        _canvasClickerMode.SetActive(true);

    }

    // Logic when player turned off clicker mode
    private void ToggleOff()
    {
        _canvasClickerMode.SetActive(false);
        _canvasMain.SetActive(true);
        CheckStatus.ClickerHasRun = false;
        isClickerModeEnable = false;
    }

    private void SellItems()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        itemsToSell = _onSaleFrameRepository.GetAll();

        if (itemsToSell.Count == 0)
        {
            Debug.Log("��� �������� �������. " + _clickCount);
            _listNull = true;
            return;
        }

        if (!_listNull)
        {
            Debug.Log("Start");
            foreach (ModelsOnSaleFrame item in itemsToSell)
            {
                int chance = Convert.ToInt32(item.liquidity * 100 * item.buffLiquidity);

                int resultRandom = Random.Range(1, 100);

                if (resultRandom <= chance)
                {
                    if (item.countProduct > 0)
                    {
                        Debug.Log("������ ����� �� �������� " + item.nameProduct + " ���� " + _clickCount);
                        item.countProduct--;
                        _playerData = PlayerDataHolder.playerData;
                        _playerData.AddCoins(item.priceProduct);
                        _playerData.AddExperience();

                        Debug.Log($"idSell: {item.idSell} ��������: {item.countProduct}  ����: {_clickCount}");
                    }
                }
                else
                {
                    Debug.Log("������� �� �������� " + item.nameProduct + " �� �������." + " ���� " + _clickCount);
                }

             
            }
                       
            _onSaleFrameRepository.SaveDataList(itemsToSell);
        }

        stopwatch.Stop();
        Debug.Log("����� ���������� SellItems: " + stopwatch.ElapsedMilliseconds + " �����������." + _clickCount);

        Debug.Log("////////////////////////////////////////////////////////////////////////////////////" + _clickCount);
    }
}



