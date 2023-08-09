using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.WareHouse;
using Assets.Scripts.Architecture.WareHouseDb;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptSaleFrame : MonoBehaviour
{

    [SerializeField] private GameObject _itemProduct;
    private SellFrameRepository _saleFrameRepository;


    // Start is called before the first frame update
    void Start()
    {
        _saleFrameRepository = new SellFrameRepository(new SellFrameDbMock());
    }

    // Update is called once per frame
    void Update()
    {
        DisplayProduct();
    }


    void DisplayProduct()
    {

        List<ModelsSaleFrame> allItems = _saleFrameRepository.GetAll();

        GameObject itemProduct = transform.GetChild(0).gameObject;
        GameObject elementItem;


        for (int i = 0; i < allItems.Count; i++)
        {
                elementItem = Instantiate(itemProduct, transform);

                // Установка спрайта
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].price.ToString();
                elementItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = allItems[i].countProduct.ToString();

                elementItem.transform.GetChild(4).GetComponent<Button>().AddEventListener(allItems[i].idProduct, ItemClicked);
        }

        _itemProduct.SetActive(false);

    }


    void ItemClicked(int idProduct, Button button)
    {

        Debug.Log("Item with id " + idProduct + " clicked");

        /*Debug.Log(_buyFrameRepository.BuyItem(idProduct, _playerData.Coins));

        _test = new WareHouseRepository(new WareHouseDbMock());

        List<ModelWareHouse> items = _test.GetAll();

        Debug.Log("");*/

    }

}
