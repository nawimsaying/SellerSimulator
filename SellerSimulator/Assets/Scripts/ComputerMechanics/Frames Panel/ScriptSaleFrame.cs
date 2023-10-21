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
    [SerializeField] private GameObject _popWindow;
    private int _currentSliderValue = 0;
    private List<GameObject> displayedItems = new List<GameObject>();



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
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{allItems[i].countProduct} шт. ";

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForBuyFrame(() => {
                    LoadInfoPopWindow(tempIndex, allItems);
                    PlayerPrefs.SetInt("idForCounter", tempIndex);
                    PlayerPrefs.Save();
                });

                displayedItems.Add(elementItem);

                /* Slider slider = elementItem.transform.GetChild(4).GetComponent<Slider>();
                 slider.maxValue = allItems[i].countProduct;*/

                /*elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForSaleFrame(() => ItemClicked(allItems[tempIndex].idBox, Convert.ToInt32(slider.value)));*/
            }

            _itemProduct.SetActive(false);
        }
        else
            _itemProduct.SetActive(false);
    }


    public void LoadInfoPopWindow(int id, List<ModelsSaleFrame> allItems)
    {
       

        // Обновите содержимое существующего окна
        _popWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[id].productName;
        _popWindow.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[id].imageName);
        _popWindow.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = 0.ToString();
        Slider slider = _popWindow.transform.GetChild(10).GetComponent<Slider>();
        slider.value = 0;
        slider.maxValue = allItems[id].countProduct;     

        Button buttonSell = _popWindow.transform.GetChild(4).GetComponent<Button>();
        buttonSell.onClick.RemoveAllListeners(); // Удалить все предыдущие обработчики
        buttonSell.onClick.AddListener(() => ButtonSellClicked(allItems[id].idProduct, Convert.ToInt32(slider.value)));

        _popWindow.SetActive(true);

    }

    public void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear(); // Очищаем список после удаления элементов
        _itemProduct.SetActive(true);
    }



    void ButtonSellClicked(int idProduct, int currentSliderValue)
    {
        Debug.Log("Item with id " + idProduct + "value slider" + currentSliderValue);
        bool result = _saleFrameRepository.PutOnSale(idProduct, currentSliderValue);
                
        if (result)
        {
            ClearDisplayedItems();
            DisplayProduct();
        }
        _popWindow.SetActive(false);
    }

    void ButtonInstantSell(int idProduct, int currentSliderValue)
    {

    }

}