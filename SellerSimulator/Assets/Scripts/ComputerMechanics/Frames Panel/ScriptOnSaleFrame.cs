using Assets.Scripts.Architecture.DataBases.AdvertisingDb;
using Assets.Scripts.Architecture.OnSaleFrame;
using Assets.Scripts.Architecture.WareHouse;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScriptOnSaleFrame : MonoBehaviour
{
    [SerializeField] private GameObject _itemProduct;
    [SerializeField] private GameObject _itemAds;
    [SerializeField] private GameObject _tabOnSaleFrame;
    [SerializeField] private GameObject _popWindow;
    [SerializeField] private ComputerPopWindowController _popWindowManager;
    private OnSaleFrameRepository _onSaleFrameRepository;
    private PlayerData _playerData;
    private List<GameObject> displayedItems = new List<GameObject>();
    private List<GameObject> displayedAds = new List<GameObject>();
    private List<ModelsOnSaleFrame> _tempAllItems;
    private List<ModelsOnSaleFrame> allItems;


    void OnEnable()
    {
        DisplayProduct();
    }

    void DisplayProduct()
    {
        // Очищаем предыдущие элементы
        ClearDisplayedItems();

        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        allItems = _onSaleFrameRepository.GetAll();

        if (allItems.Count > 0)
        {
            GameObject itemProduct = transform.GetChild(0).gameObject;
            GameObject elementItem;

            for (int i = 0; i < allItems.Count; i++)
            {
                int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].nameProduct;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"На продаже {allItems[i].countProduct} шт. ";

                Button button = elementItem.transform.GetChild(3).GetComponent<Button>();


                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => {
                    if (!allItems[tempIndex].bufAds)
                    {
                        LoadInfoPopWindow(tempIndex, allItems);
                    }
                });

                button.interactable = !allItems[tempIndex].bufAds;

                if (allItems[tempIndex].bufAds)
                {
                    // Если bufAds = true, меняем текст и спрайт
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "Активировано";
                    button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Btn_ActiveAds");
                }

                displayedItems.Add(elementItem);
            }

            _itemProduct.SetActive(false);
        }
        else
        {
            _itemProduct.SetActive(false);
        }
    }



    public void LoadInfoPopWindow(int id, List<ModelsOnSaleFrame> allItems)
    {
        ClearDisplayedAds();
        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        List<ModelAdvertising> listAds = _onSaleFrameRepository.GetAllAds();

        if (listAds.Count > 0)
        {
            Transform contentTransform = _popWindow.transform.Find("Content");

            if (contentTransform != null)
            {
                GameObject elementItem;

                for (int i = 0; i < listAds.Count; i++)
                {
                    int tempIndex = i;
                    elementItem = Instantiate(contentTransform.GetChild(0).gameObject, contentTransform);

                    elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(listAds[i].imageName);
                    TextMeshProUGUI textName = elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    textName.text = listAds[i].nameAds.ToString();

                    Button button = elementItem.transform.GetChild(3).GetComponent<Button>();
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    if (listAds[i].priceWatchAds)
                    {
                        buttonText.text = "Смотреть рекламу";
                    }
                    else
                    {
                        RectTransform textRectTransform = buttonText.GetComponent<RectTransform>();
                        textRectTransform.anchoredPosition += new Vector2(-10f, 0f);
                        buttonText.text = listAds[i].goldenPrice.ToString();
                        GameObject goldIcon = new GameObject("GoldIcon", typeof(Image));
                        goldIcon.transform.SetParent(button.transform, false);
                        RectTransform rectTransform = goldIcon.GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(16.5f, 1.8f); // Измените смещение по X, чтобы подвинуть вправо
                        rectTransform.sizeDelta = new Vector2(26, 26); // Измените размер изображения золота

                        Image goldImage = goldIcon.GetComponent<Image>();
                        goldImage.sprite = Resources.Load<Sprite>("goldSprite");
                    }

                    elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = listAds[i].description;

                    button.onClick.AddListener(() => {
                        ItemCliked(allItems[id], listAds[tempIndex]);
                    });

                    displayedAds.Add(elementItem);
                }
            }
            else
            {
                Debug.LogError("Content not found in PopWindow!");
            }
        }
        else
        {
            Debug.LogError("No ads found!");
        }

        _itemAds.SetActive(false);

        Button buttonClose = _popWindow.transform.GetChild(1).GetComponent<Button>();
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(() => _popWindowManager.ClosePopWindowForOnSale());

        _popWindowManager.OpenPopWindowForOnSale();
    }


    void ClearDisplayedAds()
    {
        foreach (var item in displayedAds)
        {
            Destroy(item);
        }

        displayedAds.Clear();
        _itemAds.SetActive(true);
    }

    void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear();
        _itemProduct.SetActive(true);
    }

    public void ItemCliked(ModelsOnSaleFrame saleItem, ModelAdvertising ads)
    {
        Debug.Log("Нажал кнопку: " + ads);

        bool result = _onSaleFrameRepository.SetBuffForItem(saleItem, ads);
        
        if(!result)
        {
            _playerData = PlayerDataHolder.playerData;
            if(_playerData.Gold < ads.goldenPrice)
            {
                Debug.Log("not enough Gold");
            }
        }
        
        ClearDisplayedItems();
        DisplayProduct();
         

        _popWindowManager.ClosePopWindowForOnSale();
    }
}
