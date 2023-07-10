using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WarehouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject canvasAddPrefab;
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject buttonAddPrefab;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Vector3[] spawnPosition;

    [NonSerialized] public static int[] frameSamples_1 = { 0, 0 };

    private static GameObject buttonAddPrefabStatic;

    private void Start()
    {
        canvasMain.SetActive(true);
        canvasAddPrefab.SetActive(false);

        buttonAddPrefabStatic = buttonAddPrefab;
    }

    // Отвечает за нажатие кнопки плюс
    public void OpenCanvasAddPrefab()
    {
        canvasMain.SetActive(false);
        canvasAddPrefab.SetActive(true);
    }

    public void CloseCanvasAddPrefab()
    {
        canvasMain.SetActive(true);
        canvasAddPrefab.SetActive(false);
    }

    // Спавним шаблон
    public void CreateSample(int idSample)
    {
        Instantiate(prefabs[idSample], spawnPosition[idSample], Quaternion.Euler(0f, -90f, 0f));

        frameSamples_1[CameraWarehouse.GetCameraPosition()] = 1;

        ChangeActiveButtonPlus();
    }

    public static void ChangeActiveButtonPlus()
    {
        if (frameSamples_1[CameraWarehouse.GetCameraPosition()] == 1)
            buttonAddPrefabStatic.SetActive(false);
        else
            buttonAddPrefabStatic.SetActive(true);
    }
}
