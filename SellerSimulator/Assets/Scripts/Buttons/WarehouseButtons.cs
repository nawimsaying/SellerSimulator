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

    private static string[] _samplingFrame; // �������� �� ��, ���� �� �� ������ ������ ��� �� ������

    private static GameObject _buttonEditStatic;

    private bool _isSet = false; // ����� ����� �����������, ����������������� �� ������ _isSamplingFrame

    private List<string> _namesOfSamples = new List<string>() { "rack", "pallet" }; // ���� �� ����������� ���������� ��������

    private void Start()
    {
        StartCoroutine(SetValueSamplingFrame());

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonEditStatic = _buttonEdit;
    }

    // �������� �� ������� ������ ����
    public void OpenCanvasAddPrefab()
    {
        _canvasMain.SetActive(false);
        _canvasAddPrefab.SetActive(true);
    }

    // ��������� ������ � ���������
    public void CloseCanvasAddPrefab()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);
    }

    // ������� ������
    public void CreateSample(int idSample)
    {
        if (_samplingFrame[CameraWarehouse.GetCameraPosition()] == "none")
        {
            Instantiate(_prefabs[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // ���������, ��� � ������ ������ ��� ��������� �����-�� ������
            if (idSample == 0)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[0];
            else if (idSample == 1)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[1];

            Debug.Log("Set sample - " + _samplingFrame[CameraWarehouse.GetCameraPosition()]);
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    // �������������� ������ � ��������� � ������� ��������
    private IEnumerator SetValueSamplingFrame()
    {
        while (CameraWarehouse.countOfFrames == 0)
            yield return null;

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        // ������� - ��� ������ ������ �� ���� ����� ������ ������������ ������������ ��� �������� �� �������
        // � ��� ��������� ������������ �� ��� ����� � ������� isSamplingFrame ������ ���� ��������� ��������
        // ���� ��������� ������ ���������� False, ������� ��������, ��� � ������� ��� ��������
        for (int i = 0; i < _samplingFrame.Length; i++)
            _samplingFrame[i] = "none";

        Debug.Log("SF: " + _samplingFrame[0] + " " + _samplingFrame[1]);
    }
}
