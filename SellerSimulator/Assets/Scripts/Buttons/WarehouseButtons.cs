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

    private static bool[] _isSamplingFrame; // �������� �� ��, ���� �� �� ������ ������ ��� �� ������

    private static GameObject _buttonAddPrefabStatic;

    private bool _isSet = false; // ����� ����� �����������, ����������������� �� ������ _isSamplingFrame

    private void Start()
    {
        _isSamplingFrame = new bool[CameraWarehouse.countOfFrames];

        // ������� - ��� ������ ������ �� ���� ����� ������ ������������ ������������ ��� �������� �� �������
        // � ��� ��������� ������������ �� ��� ����� � ������� isSamplingFrame ������ ���� ��������� ��������
        // ���� ��������� ������ ���������� False, ������� ��������, ��� � ������� ��� ��������
        for (int i = 0; i < _isSamplingFrame.Length; i++)
            _isSamplingFrame[i] = false;

        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonAddPrefabStatic = _buttonAddPrefab;
    }

    private void FixedUpdate()
    {
        // ��� ��� �������� ���������� countOfFrames � ������ CameraWarehouse ��������������� �� �����, �������� �������
        // ���-�� ��������� ��������, ���� ��� ���������� �� ������� �������� �� ���������� Unity
        if (!_isSet && CameraWarehouse.countOfFrames != 0)
        {
            _isSamplingFrame = new bool[CameraWarehouse.countOfFrames];

            // ������� - ��� ������ ������ �� ���� ����� ������ ������������ ������������ ��� �������� �� �������
            // � ��� ��������� ������������ �� ��� ����� � ������� isSamplingFrame ������ ���� ��������� ��������
            // ���� ��������� ������ ���������� False, ������� ��������, ��� � ������� ��� ��������
            for (int i = 0; i < _isSamplingFrame.Length; i++)
                _isSamplingFrame[i] = false;

            _isSet = true;
        }
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
        if (!_isSamplingFrame[CameraWarehouse.GetCameraPosition()])
        {
            Instantiate(_prefabs[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // ���������, ��� � ������ ������ ��� ��������� �����-�� ������
            _isSamplingFrame[CameraWarehouse.GetCameraPosition()] = true;

            ChangeActiveButtonPlus();
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    // ������� ������ ������� ����������, ���� ����� �������� �� ����� ������ � ���������� ��, ���� �� ��� ������
    public static void ChangeActiveButtonPlus()
    {
        if (_isSamplingFrame[CameraWarehouse.GetCameraPosition()])
            _buttonAddPrefabStatic.SetActive(false);
        else
            _buttonAddPrefabStatic.SetActive(true);
    }
}
