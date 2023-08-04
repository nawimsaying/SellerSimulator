using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts.Architecture.WareHouseDb;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject _canvasAddPrefab;
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _buttonEdit;
    [SerializeField] private GameObject _spriteSmallBox;

    private static GameObject _buttonEditStatic;
    private static GameObject _spriteSmallBoxStatic;

    private SamplesController _samplesController;

    private void Start()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonEditStatic = _buttonEdit;

        _samplesController = SamplesController.Instance;

        _spriteSmallBoxStatic = _spriteSmallBox;
    }

    // Responsible for pressing the plus button
    public void OpenCanvasAddPrefab()
    {
        _canvasMain.SetActive(false);
        _canvasAddPrefab.SetActive(true);
    }

    // Closes the canvas with templates
    public void CloseCanvasAddPrefab()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);
    }

    // Spawn Template
    public void CreateSample(int idSample)
    {
        _samplesController.CreateSample(idSample);
    }

    public void SpawnBoxesInToolBar()
    {
        int smallBoxes = PlayerPrefs.GetInt("smallBoxes");
        int bigBoxes = PlayerPrefs.GetInt("bigBoxes");

        if (smallBoxes > 0 && !_spriteSmallBoxStatic.activeSelf)
        {
            _spriteSmallBoxStatic.SetActive(true);
        }
        else if (smallBoxes == 0 && _spriteSmallBoxStatic.activeSelf)
        {
            _spriteSmallBoxStatic.SetActive(false);
        }

        if (bigBoxes > 0)
        {
            // _spriteBigBoxStatic.SetActive(true);
        }
        else if (bigBoxes == 0)
        {
            // _spriteBigBoxStatic.SetActive(false);
        }
    }
}
