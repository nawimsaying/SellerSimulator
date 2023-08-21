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

public static class ButtonExtensionSaleFrame
{
    public static void AddEventListenerForSaleFrame(this Button button, Action onClick)
    {
        button.onClick.AddListener(() => {
            onClick();
        });
    }
}

public class ScriptSaleFrame : MonoBehaviour
{

    [SerializeField] private GameObject _itemProduct;
    private SellFrameRepository _saleFrameRepository;
    private SliderController _sliderController;
    private int _currentSliderValue = 0;


    // Start is called before the first frame update
    void Start()
    {
        _saleFrameRepository = new SellFrameRepository(new SellFrameDbMock());
        _sliderController = new SliderController();
        DisplayProduct();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void DisplayProduct()
    {
        List<ModelsSaleFrame> allItems = _saleFrameRepository.GetAll();

        if (allItems.Count > 0)
        {
            GameObject itemProduct = transform.GetChild(0).gameObject;
            GameObject elementItem;

            for (int i = 0; i < allItems.Count; i++)
            {
                int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                // Установка спрайта
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].countProduct.ToString();

                Slider slider = elementItem.transform.GetChild(4).GetComponent<Slider>();
                slider.maxValue = allItems[i].countProduct;

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForSaleFrame(() => ItemClicked(allItems[tempIndex].idProduct, Convert.ToInt32(slider.value)));
            }

            _itemProduct.SetActive(false);
        }
        else
            _itemProduct.SetActive(false);
    }


    void ItemClicked(int id, int currentSliderValue)
    {
        Debug.Log("Item with id " + id + "value slider" + currentSliderValue);
    }

}