using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ComputerPopWindowController : MonoBehaviour
{
    [SerializeField] private GameObject popWindowForBuy;
    [SerializeField] private GameObject popWindowForSale;
    [SerializeField] private GameObject background;

    private void Start()
    {
        popWindowForBuy.SetActive(false);
        popWindowForSale.SetActive(false);
        background.SetActive(false);
    }

    public void OpenPopWindowForBuy()
    {
        popWindowForBuy.SetActive(true);
        background.SetActive(true);
    }
    public void OpenPopWindowForSale()
    {
        popWindowForSale.SetActive(true);
        background.SetActive(true);
    }

    public void ClosePopWindowForBuy()
    {
        popWindowForBuy.SetActive(false);
        background.SetActive(false);
    }

    public void ClosePopWindowForSale()
    {
        popWindowForSale.SetActive(false);
        background.SetActive(false);
    }
}
