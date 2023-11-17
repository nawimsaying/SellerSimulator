using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButtonsFramesPanel : MonoBehaviour
{
    [SerializeField] private GameObject _frameBuy;
    [SerializeField] private GameObject _frameSale;
    [SerializeField] private GameObject _frameOnSale;
    [SerializeField] private GameObject _frameContracts;
    [SerializeField] private GameObject _frameStats;

    [SerializeField] private TextMeshProUGUI _textBanner;

    private void Start()
    {
        _textBanner.text = "Оптовый рынок";

        _frameBuy.SetActive(true);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameBuy()
    {
        _textBanner.text = "Оптовый рынок";

        _frameBuy.SetActive(true);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameSale()
    {
        _textBanner.text = "Склад";

        _frameBuy.SetActive(false);
        _frameSale.SetActive(true);
        _frameOnSale.SetActive(false);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameOnSale()
    {
        _textBanner.text = "Активные продажи";

        _frameBuy.SetActive(false);
        _frameSale.SetActive(false);
        _frameOnSale.SetActive(true);
        _frameContracts.SetActive(false);
        _frameStats.SetActive(false);
    }

    public void OpenFrameContracts()
    {
        _textBanner.text = "Контракты";

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
