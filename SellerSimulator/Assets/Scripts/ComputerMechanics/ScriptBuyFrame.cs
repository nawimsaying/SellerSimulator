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
            OnClick(param, button); // �������� button ��� ��������
        });
    }
}

public class ScriptBuyFrame : MonoBehaviour
{
    private BuyFrameRepository _buyFrameRepository;
    private WareHouseRepository _test; // ������������ ��� ��������� ������  
    private PlayerData _playerData;
    private List<int> displayedProductIds = new List<int>(); // ���� ��� ����������� ���� ������� ��� ���������� �� �������
    private int _tempLevelUser;
    private bool _deletedDemoItem; //���������� ��������� ��� ���������, ������ �� ����������� ����� item ��� ���
    [SerializeField] private GameObject _unavailableItem; //����������� product ��� user


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
            _tempLevelUser = _playerData.Level;
            DisplayProduct();
            _unavailableItem.transform.SetAsLastSibling();
        }
    }

    void DisplayProduct() 
    {
        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();

        GameObject itemProduct = transform.GetChild(0).gameObject;
        GameObject elementItem;
        GameObject lockedItemForGold = transform.GetChild(1).gameObject;
        GameObject elementItemForGold;

        for (int i = 0; i < allItems.Count; i++)
        {
            if (_playerData.Level >= allItems[i].levelUnlock && !displayedProductIds.Contains(allItems[i].idProduct) && allItems[i].lockForGold == false)
            {

                elementItem = Instantiate(itemProduct, transform);

                // ��������� �������
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].price.ToString();

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListener(allItems[i].idProduct, ItemClicked);

                displayedProductIds.Add(allItems[i].idProduct);
            }else if (_playerData.Level >= allItems[i].levelUnlock && allItems[i].lockForGold == true && !displayedProductIds.Contains(allItems[i].idProduct))
            {
                elementItemForGold = Instantiate(lockedItemForGold, transform);

                Transform childTransform = elementItemForGold.transform.GetChild(0);
                ////////////////////////////////////////////////////////////////////
                /// ���������� �������� ��������� �������� ������
                ///////////////////////////////////////////////////////////////////
                Button buttonComponent = childTransform.GetComponent<Button>();

                // �������� ��������� TextMeshProUGUI �� ������
                TextMeshProUGUI textComponent = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                // �������� ����� �� ������ ��������
                textComponent.text = allItems[i].goldenPrice.ToString();
                /////////////////////////////////////////////////////////////////////

                elementItemForGold.transform.GetChild(0).GetComponent<Button>().AddEventListener(allItems[i].idProduct, UnlockItem);

                displayedProductIds.Add(allItems[i].idProduct);

            }

            

            if (i == allItems.Count - 1)
            {
                _unavailableItem.SetActive(true);
            }
        }

        if (!_deletedDemoItem)
        {
            Destroy(lockedItemForGold);
            Destroy(itemProduct);
            _deletedDemoItem = true;
        }

    }

    // ������ ����� ���������, �� �� �� �� ������ ��������. ����� �� ������� ������ ����� �������� �����
    void ItemClicked(int idProduct, Button button)
    { 

        Debug.Log("Item with id " + idProduct + " clicked");

        Debug.Log(_buyFrameRepository.BuyItem(idProduct, _playerData.Coins));

        _test = new WareHouseRepository(WareHouseDbMock.Instance);

        List<ModelWareHouse> items = _test.GetAll();

        Debug.Log("");

    }


    void UnlockItem(int idProduct, Button button)
    {

        Debug.Log(_buyFrameRepository.UnlockItemForGold(idProduct, _playerData.Gold));

        Debug.Log("LockItem with id " + idProduct + " clicked");

        Debug.Log("");

    }
}