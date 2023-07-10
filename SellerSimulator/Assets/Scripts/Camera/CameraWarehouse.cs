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
    [NonSerialized] public static int countOfFrames; // Аналог _countOfFrames, только ее мы будем использовать в других классах

    [SerializeField] private int _countOfFrames = 0; // Отвечает за количество фреймов в текущем помещении
    [SerializeField] private float _speed = 2f; // Отвечает за скорость камеры при передвижении между фреймами
    [SerializeField] private float _totalDuration = 2f; // Отвечает за меру сглаживания движения камеры между фреймами
    [SerializeField] private GameObject _buttonLeft; // Указываем кнопку Влево
    [SerializeField] private GameObject _buttonRight; // Указываем кнопку Вправо

    private Vector3 _moveDistanceLeft = new Vector3(7, 0, 0);
    private Vector3 _moveDistanceRight = new Vector3(-7, 0, 0);

    private bool _isMoving = false;

    public static bool[] _isCameraInFrame; // С помощью этого массива отслеживаем положение камеры и, соответственно, текущего фрейма

    private void Start()
    {
        // Кэшируем переменные, чтобы их можно было использовать в других классах
        countOfFrames = _countOfFrames;

        _isCameraInFrame = new bool[countOfFrames];

        // Заполняем массив значениями False, которые означают, что камеры на данном фрейме нет
        for (int i = 0; i < _isCameraInFrame.Length; i++) 
            _isCameraInFrame[i] = false;

        // При старте указываем, что камера находится на первом фрейме
        _isCameraInFrame[0] = true;

        // Сразу деактивируем кнопку Влево, так как находимся на крайнем левом фрейме
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

    // Метод с математической функцией, которая отвечает за сглаживание движения камеры между фреймами
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    // После движения камеры между фреймами, включаем или выключаем кнопки Влево или Вправо
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
        // Двигаемся ли мы сейчас
        if (_isMoving)
            return;

        // Проверяем, можем ли мы двинуться влево
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i - 1 >= 0)
                {
                    // Сдвигаем камеру влево
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
        // Двигаемся ли мы сейчас
        if (_isMoving)
            return;

        // Проверяем, можем ли мы двинуться вправо
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i + 1 < _isCameraInFrame.Length)
                {
                    // Сдвигаем камеру вправо
                    _isCameraInFrame[i] = false;
                    _isCameraInFrame[i + 1] = true;

                    StartCoroutine(MoveToTarget(_moveDistanceRight));
                }
                else
                    return;
            }
        }
    }

    // Метод, который возвращает текущее положение камеры (то, на каком фрейме сейчас находится игрок)
    public static int GetCameraPosition()
    {
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
                return i; // Возвращаем индекс активного фрейма
        }

        return -1; // В случае ошибки возвращаем -1
    }
}
