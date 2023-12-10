using UnityEngine;


public class ComputerPopWindowController : MonoBehaviour
{
    [SerializeField] private GameObject popWindowForBuy;
    [SerializeField] private GameObject popWindowForSale;
    [SerializeField] private GameObject popWindowForOnSale;
    [SerializeField] private GameObject backgroundForBuy;
    [SerializeField] private GameObject backgroundForSale;
    [SerializeField] private GameObject backgroundForOnSale;


    private void Start()
    {
        HideAll();
    }

    public void OpenPopWindowForBuy()
    {
        popWindowForBuy.SetActive(true);
        backgroundForBuy.SetActive(true);
    }
    public void OpenPopWindowForSale()
    {
        popWindowForSale.SetActive(true);
        backgroundForSale.SetActive(true);
    }
    public void OpenPopWindowForOnSale()
    {
        popWindowForOnSale.SetActive(true);
        backgroundForOnSale.SetActive(true);
    }

    public void ClosePopWindowForBuy()
    {
        popWindowForBuy.SetActive(false);
        backgroundForBuy.SetActive(false);
    }

    public void ClosePopWindowForSale()
    {
        popWindowForSale.SetActive(false);
        backgroundForSale.SetActive(false);
    }

    public void ClosePopWindowForOnSale()
    {
        popWindowForOnSale.SetActive(false);
        backgroundForOnSale.SetActive(false);
    }
    public void HideAll()
    {
        popWindowForBuy.SetActive(false);
        popWindowForSale.SetActive(false);
        popWindowForOnSale.SetActive(false);
        backgroundForBuy.SetActive(false);
        backgroundForSale.SetActive(false);
        backgroundForOnSale.SetActive(false);
    }
}
