using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButtonsFramesPanel : MonoBehaviour
{
    [SerializeField] private GameObject _frameBuy;
    [SerializeField] private GameObject _frameSale;
    [SerializeField] private GameObject _frameOnSale;
    [SerializeField] private GameObject _frameContracts;
    [SerializeField] private GameObject _frameStats;
    // Start is called before the first frame update
    private void Start()
    {
        _frameBuy.SetActive(true);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameBuy()
    {
        _frameBuy.SetActive(true);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameSale()
    {
        _frameBuy.SetActive(false);
        _frameSale.SetActive(true);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameOnSale()
    {
        _frameBuy.SetActive(false);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(true);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameContracts()
    {
        _frameBuy.SetActive(false);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(true);
        _frameStats.SetActive(false);
    }

    public void OpenFrameStats()
    {
        _frameBuy.SetActive(false);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(true);
    }

   
}
