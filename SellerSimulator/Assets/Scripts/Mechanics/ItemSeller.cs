using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Architecture.OnSaleFrame;

public class ItemSeller : MonoBehaviour
{
    private static ItemSeller instance;
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
        if (!_isSelling && _listNull)
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
            if (!_isSelling)
            {
                _isSelling = true;
                itemsToSell = _onSaleFrameRepository.GetAll();

                if (itemsToSell.Count == 0)
                {
                    Debug.Log("��� �������� �������.");
                    _isSelling = false;
                    _listNull = true;
                    yield break;
                }

                foreach (ModelsOnSaleFrame item in itemsToSell)
                {
                    int resultRandom = Random.Range(1, 100);

                    if (resultRandom > _successThreshold)
                    {
                        if (item.countProduct > 0)
                        {
                            Debug.Log("������ ����� �� �������� " + item.nameProduct);
                            item.countProduct--;

                            Debug.Log("��������: " + item.countProduct);
                        }
                    }
                    else
                    {
                        Debug.Log("������� �� �������� " + item.nameProduct + " �� �������.");
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
}
