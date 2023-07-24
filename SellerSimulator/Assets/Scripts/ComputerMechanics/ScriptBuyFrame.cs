using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param);
            
        });
    }
}

public class ScriptBuyFrame : MonoBehaviour
{
    public BuyFrameRepository _buyFrameRepository;

    void Start()
    {
        // Создаем экземпляр класса BuyFrameRepository
        _buyFrameRepository = new BuyFrameRepository(new BuyFrameDbMock());
        List<ModelsBuyFrame> allItems = _buyFrameRepository.GetAll();

        GameObject itemProduct = transform.GetChild(0).gameObject;
        GameObject element;
;

        for (int i = 0; i < allItems.Count; i++)
        {
            element = Instantiate(itemProduct, transform);

            // Установка спрайта
            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
            element.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
            element.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].price.ToString();

            element.transform.GetChild(3).GetComponent<Button>().AddEventListener(allItems[i].idProduct, ItemClicked);
        }

        Destroy(itemProduct);
    }

    // Сейчас метод проверяет, на ту ли мы кнопку нажимаем. Затем по нажатию кнопка будет покупать товар
    void ItemClicked(int idProduct)
    {
        Debug.Log("Item with id " + idProduct + " clicked");

        Debug.Log(_buyFrameRepository.BuyItem(idProduct, 10000));

    }

    // Update is called once per frame
    void Update()
    {
    }
}