using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;
using Assets.Scripts.Player;

public class ItemSeller : MonoBehaviour
{
    private static ItemSeller instance;
    private PlayerData _playerData;
    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    private int _saleDelay = 1;
    private int _successThreshold = 1;
    private bool _isSelling;
    private bool _listNull;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
            OfflineItemSeller.SalesCompleted += StartSellingItems;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!_isSelling && _listNull && !CheckStatus.ClickerHasRun)
        {
            _listNull = false;
            StartCoroutine(SellItems());
        }
    }

    private void StartSellingItems()
    {
        CheckStatus.HasRun = true;
        StartCoroutine(SellItems());
    }

    private IEnumerator SellItems()
    {
        while (true)
        {
            if (!_isSelling && !CheckStatus.ClickerHasRun)
            {
                _isSelling = true;
                itemsToSell = _onSaleFrameRepository.GetAll();

                if (itemsToSell.Count == 0)
                {
                    Debug.Log("Все предметы проданы.");
                    _isSelling = false;
                    _listNull = true;
                    yield break;
                }

                foreach (ModelsOnSaleFrame item in itemsToSell)
                {
                    int chance = Convert.ToInt32(item.liquidity * 100 * item.buffLiquidity);
                    // contarct = 1 - 1.75
                    //поднять ликвидность изначальную
                    //int chance = 100;

                    int resultRandom = Random.Range(1, 100);

                    if (resultRandom <= chance)
                    {
                        if (item.countProduct > 0)
                        {
                            Debug.Log("Продан товар из карточки " + item.nameProduct);
                            item.countProduct--;
                            _playerData = PlayerDataHolder.playerData;
                            _playerData.AddCoins(item.priceProduct);

                            Debug.Log("Осталось: " + item.countProduct);
                        }
                    }
                    else
                    {
                        Debug.Log("Продажа из карточки " + item.nameProduct + " не удалась.");
                    }
                }

                _onSaleFrameRepository.SaveDataList(itemsToSell);
                yield return new WaitForSeconds(_saleDelay);
                _isSelling = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    private int SalePrice(int countProduct, int priceBox)
    {
        double result = (priceBox / countProduct) * 1.2;

        return Convert.ToInt32(result);
    }

}
