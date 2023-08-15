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
public class ItemClickParametersSaleFrame
{
    public int IdProduct { get; }
    public ItemClickParametersSaleFrame(int idProduct)
    {
        IdProduct = idProduct;
        
    }
}

public class ScriptSaleFrame : MonoBehaviour
{

    [SerializeField] private GameObject _itemProduct;
    private SellFrameRepository _saleFrameRepository;
    [SerializeField] public InputField inputPriceSale;
    [SerializeField] public InputField inputCountSale;

    // Start is called before the first frame update
    void Start()
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

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenerForSaleFrame(() => ItemClicked(new ItemClickParametersSaleFrame(allItems[tempIndex].idProduct))); //доделать 
            }

            _itemProduct.SetActive(false);
        }else
            _itemProduct.SetActive(false);

    }


    void ItemClicked(ItemClickParametersSaleFrame parameters)
    {
        Debug.Log("Item with id " + parameters.IdProduct + " clicked");

        // Получаем текст из полей ввода
        string priceText = inputPriceSale.text;
        string countText = inputCountSale.text;

        // Преобразуем текст в числа (предполагается, что это целочисленные значения)
        int price;
        int count;

        if (int.TryParse(priceText, out price) && int.TryParse(countText, out count))
        {
            //_saleFrameRepository.SellItem(parameters.IdProduct, price, count);
            Debug.Log("Id: " + parameters.IdProduct + "price: " +  price + "count: " + count);
            // Теперь у вас есть id товара, цена и количество для передачи в метод SellItem
        }
        else
        {
            Debug.LogError("Invalid input for price or count");
        }
    }

}
