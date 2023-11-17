using Assets.Scripts.Architecture.OnSaleFrame;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptOnSaleFrame : MonoBehaviour
{
    [SerializeField] private GameObject _itemProduct;
    [SerializeField] private GameObject _tabOnSaleFrame;
    private OnSaleFrameRepository _onSaleFrameRepository;
    private List<GameObject> displayedItems = new List<GameObject>();
    private List<ModelsOnSaleFrame> _tempAllItems;
    private List<ModelsOnSaleFrame> allItems;


    void OnEnable()
    {
        DisplayProduct();
    }

    void DisplayProduct()
    {
        // Очищаем предыдущие элементы
        ClearDisplayedItems();

        _onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
        allItems = _onSaleFrameRepository.GetAll();

        if (allItems.Count > 0)
        {
            GameObject itemProduct = transform.GetChild(0).gameObject;
            GameObject elementItem;

            for (int i = 0; i < allItems.Count; i++)
            {
                int tempIndex = i;
                elementItem = Instantiate(itemProduct, transform);

                elementItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconProducts/" + allItems[i].imageName);
                elementItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allItems[i].nameProduct;
                elementItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"На продаже {allItems[i].countProduct} шт. ";

                elementItem.transform.GetChild(3).GetComponent<Button>().AddEventListenere(() => {
                    ItemCliked(tempIndex);
                });

                displayedItems.Add(elementItem);
            }

            _itemProduct.SetActive(false);
        }
        else
        {
            _itemProduct.SetActive(false);
        }

    }

    void ClearDisplayedItems()
    {
        foreach (var item in displayedItems)
        {
            Destroy(item);
        }

        displayedItems.Clear();
        _itemProduct.SetActive(true);
    }

    public void ItemCliked(int id)
    {
        Debug.Log("Нажал кнопку: " + id);
    }
}
