using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.WareHouseDb;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public static class ButtonExtensionBuyFrame
{
    public static void AddEventListenerForBuyFrame(this Button button, Action onClick)
    {
        button.onClick.AddListener(() => {
            onClick();
        });
    }
}


public class ScriptFrameBuy : MonoBehaviour
{
    private BuyFrameRepository _buyFrameRepository;
    private WareHouseRepository _test; // используется для временных тестов  
    private PlayerData _playerData;
    private List<int> displayedProductIds = new List<int>(); // Лист для запоминания айди который уже отображены на дисплеи
    private int _tempLevelUser;
    private bool _deletedDemoItem; //Переменная служающая для понимания, удален ли изначальный макет item или нет
    [SerializeField] private GameObject _itemProduct;
    [SerializeField] private GameObject _itemProductForGold;
    [SerializeField] private GameObject _unavailableItem; //Недоступный product для user
    [SerializeField] private GameObject _popWindow;
    [SerializeField] private TextMeshProUGUI _counterForWindowPop;
    [SerializeField] private ComputerPopWindowController _popWindowManager;
    private ClickButtonPopWindow clickButtonPopWindow;
    private List<GameObject> displayedItems = new List<GameObject>(); // Список для хранения созданных элементов
    string tempTextFromCounter;


    void Start()
    {
        clickButtonPopWindow = FindObjectOfType<ClickButtonPopWindow>();
        tempTextFromCounter  = _counterForWindowPop.text;
        _unavailableItem.SetActive(false);
        _buyFrameRepository = new BuyFrameRepository(new BuyFrameDbMock());
        _playerData = PlayerDataHolder.playerData;

        _tempLevelUser = _playerData.Level;

        DisplayProduct();

        _unavailableItem.transform.SetAsLastSibling();
    }

    void Update()
    {
        if (_playerData.Level > _tempLevelUser)
        {
            _tempLevelUser = _playerData.Level;
            ClearDisplayedItems();
            DisplayProduct();
            _unavailableItem.transform.SetAsLastSibling();
        }

        
        
        if(tempTextFromCounter != _counterForWindowPop.text)
        {
            tempTextFromCounter = _counterForWindowPop.text;
            int id = PlayerPrefs.GetInt("idForCounter");
            LoadInfoPopWindow(id);
        }
    }

    void DisplayProduct()
    {
        displayedProductIds.Clear();


        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();
        GameObject itemProduct = transform.GetChild(0).gameObject;
        GameObject elementItem;
        GameObject lockedItemForGold = transform.GetChild(1).gameObject;
        GameObject elementItemForGold;

        for (int i = 0; i < allItems.Count; i++)
        {
            int tempIndex = i;

            if (_playerData.Level >= allItems[i].levelUnlock && !displayedProductIds.Contains(allItems[i].idProduct) && allItems[i].lockForGold == false)
            {

                elementItem = Instantiate(itemProduct, transform);
                displayedItems.Add(elementItem);

                // Установка спрайта
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/"+allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].price.ToString();

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForBuyFrame(() => {
                    clickButtonPopWindow.ResetCounter();
                    LoadInfoPopWindow(tempIndex);
                    PlayerPrefs.SetInt("idForCounter", tempIndex);
                    PlayerPrefs.Save();
                });
                //elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForBuyFrame(() => ItemClicked(allItems[tempIndex].idProduct));

                displayedProductIds.Add(allItems[i].idProduct);
            }
            else if (_playerData.Level >= allItems[i].levelUnlock && allItems[i].lockForGold == true && !displayedProductIds.Contains(allItems[i].idProduct))
            {
                elementItemForGold = Instantiate(lockedItemForGold, transform);
                displayedItems.Add(elementItemForGold);

                Transform childTransform = elementItemForGold.transform.GetChild(0);
                ////////////////////////////////////////////////////////////////////
                /// Присвоение значения дочернему элементу кнопки
                ///////////////////////////////////////////////////////////////////
                Button buttonComponent = childTransform.GetComponent<Button>();

                // Получаем компонент TextMeshProUGUI из кнопки
                TextMeshProUGUI textComponent = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                // Изменяем текст на нужное значение
                textComponent.text = allItems[i].goldenPrice.ToString();
                /////////////////////////////////////////////////////////////////////

                elementItemForGold.transform.GetChild(0).GetComponent<Button>().AddEventListenerForBuyFrame(() => UnlockItem(allItems[tempIndex].idProduct));

                displayedProductIds.Add(allItems[i].idProduct);

            }

            if (i == allItems.Count - 1)
            {
                _unavailableItem.SetActive(true);
            }
        }

        _itemProduct.SetActive(false);
        _itemProductForGold.SetActive(false);
    }

    public void LoadInfoPopWindow(int id)
    {
        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();

        // Обновите содержимое существующего окна
        _popWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[id].productName;
        _popWindow.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[id].imageName);
        string textFromCounter = _counterForWindowPop.text;
        int result = allItems[id].price * int.Parse(textFromCounter);
        Button button = _popWindow.transform.GetChild(5).GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = result.ToString();


        Button buttonBuy = _popWindow.transform.GetChild(5).GetComponent<Button>();
        buttonBuy.onClick.RemoveAllListeners(); // Удалить все предыдущие обработчики
        buttonBuy.onClick.AddListener(() => ItemClicked(allItems[id].idProduct, int.Parse(_counterForWindowPop.text), result));

        Button buttonClose = _popWindow.transform.GetChild(0).GetComponent<Button>();
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(() => _popWindowManager.ClosePopWindowForBuy());

        _popWindowManager.OpenPopWindowForBuy();

    }


    public void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear(); // Очищаем список после удаления элементов
        displayedProductIds.Clear();
        _itemProduct.SetActive(true);
        _itemProductForGold.SetActive(true);
    }

    // Сейчас метод проверяет, на ту ли мы кнопку нажимаем. Затем по нажатию кнопка будет покупать товар
    void ItemClicked(int idProduct, int countProducts, int priceProducts)
    {
        Debug.Log("Item with id " + idProduct + " " + countProducts + " " + priceProducts+ " clicked");

        _buyFrameRepository.BuyItem(idProduct, countProducts, priceProducts, _playerData.Coins);
        _popWindowManager.ClosePopWindowForBuy();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// For test. How much count box on WareHouse
        ////////////////////////////////////////////////////////////////////////////////////////////////
        _test = new WareHouseRepository(new WareHouseDbMock());
        List<ModelWareHouse> items = _test.GetAll();
        ////////////////////////////////////////////////////////////////////////////////////////////////
        Debug.Log("");
    }


    void UnlockItem(int idProduct)
    {
        Debug.Log("LockItem with id " + idProduct + " clicked");

        bool result = _buyFrameRepository.UnlockItemForGold(idProduct, _playerData.Gold);

        if (result)
        {
            ClearDisplayedItems();
            DisplayProduct();
            _unavailableItem.transform.SetAsLastSibling();
        }

        Debug.Log($"Покупка{result}");
    }
}