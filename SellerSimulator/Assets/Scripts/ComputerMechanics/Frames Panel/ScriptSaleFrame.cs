using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.WareHouse;
using Assets.Scripts.Architecture.WareHouseDb;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtensionSaleFrame
{

}

public class ScriptSaleFrame : MonoBehaviour
{
    [SerializeField] private GameObject _itemProduct;
    private SellFrameRepository _saleFrameRepository;
    [SerializeField] private GameObject _popWindow;
    private int _currentSliderValue = 0;
    private List<GameObject> displayedItems = new List<GameObject>();
    [SerializeField] private ComputerPopWindowController _popWindowManager;
    private int _currentPriceProducts;
    private int _currentInstantPriceProducts;

    // Start is called before the first frame update
    void OnEnable()
    {
        _saleFrameRepository = new SellFrameRepository(new SellFrameDbMock());
        DisplayProduct();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayProduct()
    {
        ClearDisplayedItems();

        List<ModelsSaleFrame> allItems = _saleFrameRepository.GetAll();

        if (allItems.Count > 0)
        {
            GameObject itemProduct = transform.GetChild(0).gameObject;
            GameObject elementItem;

            for (int i = 0; i < allItems.Count; i++)
            {
                int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                if (allItems[i].liquidity <= 0.1)
                {
                    elementItem.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (allItems[i].liquidity >= 0.11)
                {
                    elementItem.transform.GetChild(1).gameObject.SetActive(true);
                }

                elementItem.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[i].imageName);
                elementItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = $"{allItems[i].countProduct} шт. ";

                elementItem.transform.GetChild(5).GetComponent<Button>().AddEventListenere(() => {
                    LoadInfoPopWindow(tempIndex, allItems);
                    PlayerPrefs.SetInt("idForCounter", tempIndex);
                    PlayerPrefs.Save();
                });

                displayedItems.Add(elementItem);
            }

            _itemProduct.SetActive(false);
        }
        else
        {
            ClearDisplayedItems();
            _itemProduct.SetActive(false);
        }
    }

    public void LoadInfoPopWindow(int id, List<ModelsSaleFrame> allItems)
    {
        _popWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[id].productName;
        _popWindow.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[id].imageName);
        _popWindow.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = 0.ToString();
        _popWindow.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = $"На складе: <color=#8F4B0D>{allItems[id].countProduct}</color> шт."; 
        Slider slider = _popWindow.transform.GetChild(10).GetComponent<Slider>();
        slider.value = 0;
        slider.maxValue = allItems[id].countProduct;

        UpdatePrices(slider, allItems[id]);

        slider.onValueChanged.AddListener(delegate { UpdatePrices(slider, allItems[id]); });

        //Button
        Button buttonInstantSell = _popWindow.transform.GetChild(5).GetComponent<Button>();
        buttonInstantSell.onClick.RemoveAllListeners(); // ������� ��� ���������� �����������
        buttonInstantSell.onClick.AddListener(() => ButtonInstantSell(allItems[id].idProduct, Convert.ToInt32(slider.value), _currentInstantPriceProducts));

        Button buttonSell = _popWindow.transform.GetChild(4).GetComponent<Button>();
        buttonSell.onClick.RemoveAllListeners(); // ������� ��� ���������� �����������
        buttonSell.onClick.AddListener(() => ButtonSellClicked(allItems[id].idProduct, Convert.ToInt32(slider.value)));

        Button buttonClose = _popWindow.transform.GetChild(0).GetComponent<Button>();
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(() => _popWindowManager.ClosePopWindowForSale());

        _popWindowManager.OpenPopWindowForSale();
    }

    private void UpdatePrices(Slider slider, ModelsSaleFrame saleFrame)
    {
        int priceProducts = SalePrice(saleFrame.countProduct, saleFrame.price) * (int)slider.value;
        int instantPriceProducts = InstantSalePrice(saleFrame.countProduct, saleFrame.price) * (int)slider.value;

        _currentPriceProducts = priceProducts;
        _currentInstantPriceProducts = instantPriceProducts;


        _popWindow.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = ((int)slider.value).ToString();
        _popWindow.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = $"Продажа: <color=#3A8B2D>${priceProducts}</color>";
        _popWindow.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = $"Моментально: <color=#B63636>${instantPriceProducts}</color>";
    }

    private int InstantSalePrice(int countProduct, int priceBox)
    {
        double result = (priceBox / countProduct) * 0.8;

        return Convert.ToInt32(result);
    }

    private int SalePrice(int countProduct, int priceBox)
    {
        double result = (priceBox / countProduct) * 1.2;

        return Convert.ToInt32(result);
    }

    public void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear();
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

        _popWindowManager.ClosePopWindowForSale();
    }

    void ButtonInstantSell(int idProduct, int currentSliderValue, int currentPrice)
    {
        Debug.Log("Click" + idProduct + " " + currentSliderValue);
        bool result = _saleFrameRepository.InstantSale(idProduct, currentSliderValue, currentPrice);

        if (result)
        {
            ClearDisplayedItems();
            DisplayProduct();
        }

        _popWindowManager.ClosePopWindowForSale();
    }
}
