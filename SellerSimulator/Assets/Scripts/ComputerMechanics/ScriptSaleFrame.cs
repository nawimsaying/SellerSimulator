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

public class ItemClickParametersSaleFrame
{
    public int IdProduct { get; }
    public int PriceSale { get; }
    public int CountProduct { get; }

    public ItemClickParametersSaleFrame(int idProduct, int priceSale, int countItem)
    {
        IdProduct = idProduct;
        PriceSale = priceSale;
        CountProduct = countItem;
    }
}

public class ScriptSaleFrame : MonoBehaviour
{

    [SerializeField] private GameObject _itemProduct;
    private SellFrameRepository _saleFrameRepository;
   /* [SerializeField] public GameObject inputPriceSale;
    [SerializeField] public GameObject inputCountSale;*/ //переделать

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

        /*int tempPrice = Convert.ToInt32(inputPriceSale);
        int tempCount = Convert.ToInt32(inputCountSale);*/  
           


        for (int i = 0; i < allItems.Count; i++)
        {
            int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                // Установка спрайта
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].productName;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allItems[i].countProduct.ToString();

                //elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListener(() => ItemClicked(new ItemClickParametersSaleFrame(allItems[tempIndex].idProduct, tempPrice, tempCount))); //доделать 
        }

        _itemProduct.SetActive(false);

    }


    void ItemClicked(ItemClickParametersSaleFrame parameters)
    {

        Debug.Log("Item with id " + parameters.IdProduct + " clicked");

        int temp= parameters.PriceSale;
        int temp1 = parameters.CountProduct;

        /*Debug.Log(_buyFrameRepository.BuyItem(idProduct, _playerData.Coins));

        _test = new WareHouseRepository(new WareHouseDbMock());

        List<ModelWareHouse> items = _test.GetAll();*/

        Debug.Log("");

    }

}
