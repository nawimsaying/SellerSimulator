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
    [SerializeField] private GameObject _spriteBigBox;
    [SerializeField] private GameObject _prefabRack;
    [SerializeField] private GameObject _prefabPallet;
    [SerializeField] private GameObject _buttonDelPrefab1;
    [SerializeField] private GameObject _buttonAddPrefab1;
    [SerializeField] private GameObject _nonActiveGround1;
    [SerializeField] private GameObject _buttonDelPrefab2;
    [SerializeField] private GameObject _buttonAddPrefab2;
    [SerializeField] private GameObject _nonActiveGround2;

    private static GameObject _buttonEditStatic;
    private static GameObject _spriteSmallBoxStatic;
    private static GameObject _spriteBigBoxStatic;
    private static GameObject _buttonDelPrefab1Static;
    private static GameObject _buttonAddPrefab1Static;
    private static GameObject _nonActiveGround1Static;
    private static GameObject _buttonDelPrefab2Static;
    private static GameObject _buttonAddPrefab2Static;
    private static GameObject _nonActiveGround2Static;

    private SamplesController _samplesController;

    private void Start()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonEditStatic = _buttonEdit;

        _samplesController = new SamplesController();

        _spriteSmallBoxStatic = _spriteSmallBox;
        _spriteBigBoxStatic = _spriteBigBox;

        _buttonDelPrefab1Static = _buttonDelPrefab1;
        _buttonAddPrefab1Static = _buttonAddPrefab1;
        _nonActiveGround1Static = _nonActiveGround1;
        _buttonDelPrefab2Static = _buttonDelPrefab2;
        _buttonAddPrefab2Static = _buttonAddPrefab2;
        _nonActiveGround2Static = _nonActiveGround2;
    }

    // Responsible for pressing the plus button
    public void OpenCanvasAddPrefab()
    {
        _canvasMain.SetActive(false);
        _canvasAddPrefab.SetActive(true);

        ChangePrefabActive();
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

    public void DeleteSample()
    {
        _samplesController.DeleteSample();
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

        if (bigBoxes > 0 && !_spriteBigBoxStatic.activeSelf)
        {
            _spriteBigBoxStatic.SetActive(true);
        }
        else if (bigBoxes == 0 && _spriteBigBoxStatic.activeSelf)
        {
            _spriteBigBoxStatic.SetActive(false);
        }
    }

    public void ChangePrefabActive()
    {
        // Get current camera position
        int cameraPosition = CameraWarehouse.GetCameraPosition();

        List<Sample> sampleList = SaveLoadManager.LoadSampleList();

        if (sampleList.Count != 0)
        {
            bool isSeted = false;

            for (int i = 0; i < sampleList.Count; i++)
            {
                if (sampleList[i].idFrame == cameraPosition)
                {
                    if (sampleList[i].rackSample.Length == 21)
                    {
                        // Rack
                        _buttonDelPrefab1Static.SetActive(true);
                        _buttonAddPrefab1Static.SetActive(false);
                        _nonActiveGround1Static.SetActive(false);

                        _buttonDelPrefab2Static.SetActive(false);
                        _buttonAddPrefab2Static.SetActive(true);
                        _nonActiveGround2Static.SetActive(true);

                        isSeted = true;
                        break;
                    }
                    else if (sampleList[i].rackSample.Length == 6)
                    {
                        // Pallet
                        _buttonDelPrefab2Static.SetActive(true);
                        _buttonAddPrefab2Static.SetActive(false);
                        _nonActiveGround2Static.SetActive(false);

                        _buttonDelPrefab1Static.SetActive(false);
                        _buttonAddPrefab1Static.SetActive(true);
                        _nonActiveGround1Static.SetActive(true);

                        isSeted = true;
                        break;
                    }
                }
            }
            if (!isSeted)
            {
                _buttonDelPrefab2Static.SetActive(false);
                _buttonAddPrefab2Static.SetActive(true);
                _nonActiveGround2Static.SetActive(false);

                _buttonDelPrefab1Static.SetActive(false);
                _buttonAddPrefab1Static.SetActive(true);
                _nonActiveGround1Static.SetActive(false);
            }
        }
        else
        {
            _buttonDelPrefab2Static.SetActive(false);
            _buttonAddPrefab2Static.SetActive(true);
            _nonActiveGround2Static.SetActive(false);

            _buttonDelPrefab1Static.SetActive(false);
            _buttonAddPrefab1Static.SetActive(true);
            _nonActiveGround1Static.SetActive(false);
        }
    }
}
