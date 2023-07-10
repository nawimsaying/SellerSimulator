using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraWarehouse : MonoBehaviour
{
    [NonSerialized] public static int countOfFrames; // ������ _countOfFrames, ������ �� �� ����� ������������ � ������ �������

    [SerializeField] private int _countOfFrames = 0; // �������� �� ���������� ������� � ������� ���������
    [SerializeField] private float _speed = 2f; // �������� �� �������� ������ ��� ������������ ����� ��������
    [SerializeField] private float _totalDuration = 2f; // �������� �� ���� ����������� �������� ������ ����� ��������
    [SerializeField] private GameObject _buttonLeft; // ��������� ������ �����
    [SerializeField] private GameObject _buttonRight; // ��������� ������ ������

    private Vector3 _moveDistanceLeft = new Vector3(7, 0, 0);
    private Vector3 _moveDistanceRight = new Vector3(-7, 0, 0);

    private bool _isMoving = false;

    public static bool[] _isCameraInFrame; // � ������� ����� ������� ����������� ��������� ������ �, ��������������, �������� ������

    private void Start()
    {
        // �������� ����������, ����� �� ����� ���� ������������ � ������ �������
        countOfFrames = _countOfFrames;

        _isCameraInFrame = new bool[countOfFrames];

        // ��������� ������ ���������� False, ������� ��������, ��� ������ �� ������ ������ ���
        for (int i = 0; i < _isCameraInFrame.Length; i++) 
            _isCameraInFrame[i] = false;

        // ��� ������ ���������, ��� ������ ��������� �� ������ ������
        _isCameraInFrame[0] = true;

        // ����� ������������ ������ �����, ��� ��� ��������� �� ������� ����� ������
        _buttonLeft.SetActive(false);
    }

    private IEnumerator MoveToTarget(Vector3 moveDistance)
    {
        _isMoving = true;

        Vector3 _startPosition = transform.position;
        Vector3 _targetPosition = _startPosition - moveDistance;

        float _journeyLength = Vector3.Distance(_startPosition, _targetPosition);
        float _duration = _journeyLength / _speed;
        float _startTime = Time.time;
        float _elapsedTime = 0f;

        while (_elapsedTime < _duration)
        {
            float _normalizedTime = _elapsedTime / _duration;
            float _easedTime = EaseInOut(_normalizedTime);

            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _easedTime);

            _elapsedTime = Time.time - _startTime;

            yield return null;
        }

        ChangeActiveButtons();
        WarehouseButtons.ChangeActiveButtonPlus();

        _isMoving = false;
    }

    // ����� � �������������� ��������, ������� �������� �� ����������� �������� ������ ����� ��������
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    // ����� �������� ������ ����� ��������, �������� ��� ��������� ������ ����� ��� ������
    private void ChangeActiveButtons()
    {
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i - 1 >= 0)
                    _buttonLeft.SetActive(true);
                else
                    _buttonLeft.SetActive(false);

                if (i + 1 < _isCameraInFrame.Length)
                    _buttonRight.SetActive(true);
                else 
                    _buttonRight.SetActive(false);
            }
        }
    }

    public void HoldLeft()
    {
        // ��������� �� �� ������
        if (_isMoving)
            return;

        // ���������, ����� �� �� ��������� �����
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i - 1 >= 0)
                {
                    // �������� ������ �����
                    _isCameraInFrame[i] = false;
                    _isCameraInFrame[i - 1] = true;

                    StartCoroutine(MoveToTarget(_moveDistanceLeft));
                }
                else
                    return;
            }
        }
    }

    public void HoldRight()
    {
        // ��������� �� �� ������
        if (_isMoving)
            return;

        // ���������, ����� �� �� ��������� ������
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i + 1 < _isCameraInFrame.Length)
                {
                    // �������� ������ ������
                    _isCameraInFrame[i] = false;
                    _isCameraInFrame[i + 1] = true;

                    StartCoroutine(MoveToTarget(_moveDistanceRight));
                }
                else
                    return;
            }
        }
    }

    // �����, ������� ���������� ������� ��������� ������ (��, �� ����� ������ ������ ��������� �����)
    public static int GetCameraPosition()
    {
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
                return i; // ���������� ������ ��������� ������
        }

        return -1; // � ������ ������ ���������� -1
    }
}
