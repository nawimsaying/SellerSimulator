using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.OnSaleFrame;
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


public static class ButtonExtensionOnSaleFrame
{
    
}

public class ScriptOnSaleFrame : MonoBehaviour
{
    [SerializeField] private GameObject _itemProduct;
    private OnSaleFrameRepository _onSaleFrameRepository;
    private List<GameObject> displayedItems = new List<GameObject>(); // ������ ��� �������� ��������� ���������

    private int _tempLength;

    List<ModelsOnSaleFrame> allItems;
    void Start()
    {
        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        allItems = _onSaleFrameRepository.GetAll();
        DisplayProduct();
        
    }

    // Update is called once per frame
    void Update()
    {
        allItems = _onSaleFrameRepository.GetAll();
        if (allItems.Count > _tempLength)
        {
            ClearDisplayedItems();
            DisplayProduct();
        }
                         
    }


    void DisplayProduct()
    {       
        
        _tempLength = allItems.Count;

        if (allItems.Count > 0)
        {
            GameObject itemProduct = transform.GetChild(0).gameObject;
            GameObject elementItem;

            for (int i = 0; i < allItems.Count; i++)
            {
                int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                // ��������� �������
                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].nameProduct;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"На продаже {allItems[i].countProduct} шт. ";

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenere(() => {
                    ItemCliked(tempIndex);
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


    public void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear(); // ������� ������ ����� �������� ���������
        _itemProduct.SetActive(true);
    }

    public void ItemCliked(int id)
    {

        Debug.Log("Нажал кнопку: " + id);
    }


}
