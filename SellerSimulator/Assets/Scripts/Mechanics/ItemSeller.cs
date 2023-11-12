using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;
using System.Runtime.CompilerServices;

public class ItemSeller : MonoBehaviour
{
    private OnSaleFrameRepository _onSaleFrameRepository;
    public List<ModelsOnSaleFrame> itemsToSell;
    private int _saleDelay = 1;
    private int _successThreshold = 1;
    private bool _isSelling;
    private bool _listNull;

    private void Start()
    {
        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        StartCoroutine(SellItems());
    }

    private void Update()
    {
        if (!_isSelling && _listNull)
        {
            _listNull = false;
            StartCoroutine(SellItems());
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   

    private IEnumerator SellItems()
    {
        while (true)
        {
            if (!_isSelling)
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

                List<ModelsOnSaleFrame> itemsToRemove = new List<ModelsOnSaleFrame>();

                foreach (ModelsOnSaleFrame item in itemsToSell)
                {
                    int resultRandom = Random.Range(1, 100);

                    if (resultRandom > _successThreshold)
                    {
                        if (item.countProduct > 0)
                        {
                            Debug.Log("Продан товар из карточки " + item.nameProduct);
                            item.countProduct--;

                            Debug.Log("Осталось: " + item.countProduct);

                            if (item.countProduct == 0)
                            {
                                Debug.Log("Товар из карточки " + item.nameProduct + " закончился.");
                                itemsToRemove.Add(item);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Продажа из карточки " + item.nameProduct + " не удалась.");
                    }
                }

                foreach (ModelsOnSaleFrame itemToRemove in itemsToRemove)
                {
                    itemsToSell.Remove(itemToRemove);
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
}
