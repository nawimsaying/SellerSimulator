using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WarehouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject _canvasAddPrefab;
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _buttonEdit;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Vector3[] _spawnPosition;

    private static string[] _samplingFrame; // Отвечает за то, есть ли на фрейме шаблон или он пустой

    private static GameObject _buttonEditStatic;

    private bool _isSet = false; // Нужна чтобы отслеживать, инициализировался ли массив _isSamplingFrame

    private List<string> _namesOfSamples = new List<string>() { "rack", "pallet" }; // Лист со стринговыми зазваниями шаблонов

    private void Start()
    {
        StartCoroutine(SetValueSamplingFrame());

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonEditStatic = _buttonEdit;
    }

    // Отвечает за нажатие кнопки плюс
    public void OpenCanvasAddPrefab()
    {
        _canvasMain.SetActive(false);
        _canvasAddPrefab.SetActive(true);
    }

    // Закрывает канвас с шаблонами
    public void CloseCanvasAddPrefab()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);
    }

    // Спавним шаблон
    public void CreateSample(int idSample)
    {
        if (_samplingFrame[CameraWarehouse.GetCameraPosition()] == "none")
        {
            Instantiate(_prefabs[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // Указываем, что в данном фрейме уже находится какой-то шаблон
            if (idSample == 0)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[0];
            else if (idSample == 1)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[1];

            Debug.Log("Set sample - " + _samplingFrame[CameraWarehouse.GetCameraPosition()]);
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    // Инициализируем массив с шаблонами с помощью куратины
    private IEnumerator SetValueSamplingFrame()
    {
        while (CameraWarehouse.countOfFrames == 0)
            yield return null;

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        // Костыль - при выходе игрока из этой сцены должно запоминаться расположение его шаблонов на фреймах
        // и при следующем переключении на эту сцену в массиве isSamplingFrame должны быть кастомные значения
        // Пока заполняем массив значениями False, которые означают, что в фреймах нет шаблонов
        for (int i = 0; i < _samplingFrame.Length; i++)
            _samplingFrame[i] = "none";

        Debug.Log("SF: " + _samplingFrame[0] + " " + _samplingFrame[1]);
    }
}
