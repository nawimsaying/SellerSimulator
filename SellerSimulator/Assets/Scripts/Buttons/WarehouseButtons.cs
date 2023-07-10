using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WarehouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject _canvasAddPrefab;
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _buttonAddPrefab;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Vector3[] _spawnPosition;

    private static bool[] _isSamplingFrame; // Отвечает за то, есть ли на фрейме шаблон или он пустой

    private static GameObject _buttonAddPrefabStatic;

    private bool _isSet = false; // Нужна чтобы отслеживать, инициализировался ли массив _isSamplingFrame

    private void Start()
    {
        _isSamplingFrame = new bool[CameraWarehouse.countOfFrames];

        // Костыль - при выходе игрока из этой сцены должно запоминаться расположение его шаблонов на фреймах
        // и при следующем переключении на эту сцену в массиве isSamplingFrame должны быть кастомные значения
        // Пока заполняем массив значениями False, которые означают, что в фреймах нет шаблонов
        for (int i = 0; i < _isSamplingFrame.Length; i++)
            _isSamplingFrame[i] = false;

        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonAddPrefabStatic = _buttonAddPrefab;
    }

    private void FixedUpdate()
    {
        // Так как значение переменной countOfFrames в классе CameraWarehouse устанавливается не сразу, пришлось сделать
        // что-то наподобие ожидания, пока эта переменная не получит значение из инспектора Unity
        if (!_isSet && CameraWarehouse.countOfFrames != 0)
        {
            _isSamplingFrame = new bool[CameraWarehouse.countOfFrames];

            // Костыль - при выходе игрока из этой сцены должно запоминаться расположение его шаблонов на фреймах
            // и при следующем переключении на эту сцену в массиве isSamplingFrame должны быть кастомные значения
            // Пока заполняем массив значениями False, которые означают, что в фреймах нет шаблонов
            for (int i = 0; i < _isSamplingFrame.Length; i++)
                _isSamplingFrame[i] = false;

            _isSet = true;
        }
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
        if (!_isSamplingFrame[CameraWarehouse.GetCameraPosition()])
        {
            Instantiate(_prefabs[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // Указываем, что в данном фрейме уже находится какой-то шаблон
            _isSamplingFrame[CameraWarehouse.GetCameraPosition()] = true;

            ChangeActiveButtonPlus();
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    // Убираем кнопку плюсика посередине, если игрок поставил на фрейм шаблон и показываем ее, если он его удалил
    public static void ChangeActiveButtonPlus()
    {
        if (_isSamplingFrame[CameraWarehouse.GetCameraPosition()])
            _buttonAddPrefabStatic.SetActive(false);
        else
            _buttonAddPrefabStatic.SetActive(true);
    }
}
