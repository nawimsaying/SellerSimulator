using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.WareHouseDb;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T, Button> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param, button); // Добавили button как аргумент
        });
    }
}

public class ScriptBuyFrame : MonoBehaviour
{
    private BuyFrameRepository _buyFrameRepository;
    private WareHouseRepository _test; // используется для временных тестов  
    private PlayerData _playerData;
    private List<int> displayedProductIds = new List<int>(); // Лист для запоминания айди который уже отображены на дисплеи
    private int _tempLevelUser;
    private bool _deletedDemoItem; //Переменная служающая для понимания, удален ли изначальный макет item или нет
    [SerializeField] private GameObject _unavailableItem; //Недоступный product для user


    void Start()
    {
        _unavailableItem.SetActive(false);
        _buyFrameRepository = new BuyFrameRepository(new BuyFrameDbMock());
        _playerData = PlayerDataHolder.playerData;

        _tempLevelUser = _playerData.Level;

        DisplayProduct();

        _unavailableItem.transform.SetAsLastSibling();
    }

    void Update()
    {
        if(_playerData.Level > _tempLevelUser)
        {
            DisplayProduct();
            _unavailableItem.transform.SetAsLastSibling();
            _tempLevelUser = _playerData.Level;
        }
    }

    void DisplayProduct() 
    {
        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();

        GameObject itemProduct = transform.GetChild(0).gameObject;
        GameObject element;

        for (int i = 0; i < allItems.Count; i++)
        {
            if (_playerData.Level >= allItems[i].levelUnlock && !displayedProductIds.Contains(allItems[i].idProduct))
            {

                element = Instantiate(itemProduct, transform);

                // Установка спрайта
                element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
                element.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                element.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].price.ToString();

                element.transform.GetChild(3).GetComponent<Button>().AddEventListener(allItems[i].idProduct, ItemClicked);

                displayedProductIds.Add(allItems[i].idProduct);
            }else if (i == allItems.Count - 1)
            {
                _unavailableItem.SetActive(true);
            }
        }

        if (!_deletedDemoItem)
        {
            Destroy(itemProduct);
            _deletedDemoItem = true;
        }
        
    }

    // Сейчас метод проверяет, на ту ли мы кнопку нажимаем. Затем по нажатию кнопка будет покупать товар
    void ItemClicked(int idProduct, Button button)
    { 

        Debug.Log("Item with id " + idProduct + " clicked");

        Debug.Log(_buyFrameRepository.BuyItem(idProduct, _playerData.Coins));

        _test = new WareHouseRepository(WareHouseDbMock.Instance);

        List<ModelWareHouse> items = _test.GetAll();

        Debug.Log("");

    }

}