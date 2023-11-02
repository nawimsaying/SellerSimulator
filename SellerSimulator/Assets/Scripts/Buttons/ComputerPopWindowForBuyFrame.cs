using UnityEngine;


public class ComputerPopWindowController : MonoBehaviour
{
    [SerializeField] private GameObject popWindowForBuy;
    [SerializeField] private GameObject popWindowForSale;
    [SerializeField] private GameObject backgroundForBuy;
    [SerializeField] private GameObject backgroundForSale;

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
    public void HideAll()
    {
        popWindowForBuy.SetActive(false);
        popWindowForSale.SetActive(false);
        backgroundForBuy.SetActive(false);
        backgroundForSale.SetActive(false);
    }
}
