using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _smooth = 10f;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _firstBorder = -6f;
    [SerializeField] private float _secondBorder = 6f;
    //[SerializeField] private float rotationSpeed = 0.2f;

    [NonSerialized] static public bool _isButtonPressed = false;

    private Camera _camera;
    private Transform _cameraRig;

    private Vector3 _position; // Позиция
    private Quaternion _rotation; // Вращение
    private Vector3 _localPosition; // Приблежение

    private Vector3 _startPosition;
    private Vector3 _startPositionRig;
    private Vector3 _startRotation;

    private float _yCamLimit;
    private float _zCamLimit;

    private float _xRotation = 45;
    private float _yRotation = 161.6f;
    private int _xRotationCurrent;

    void Start()
    {
        _camera = Camera.main;
        _cameraRig = transform.parent;

        _position = _cameraRig.position;
        _rotation = _cameraRig.rotation;
        _localPosition = transform.localPosition;

        _yCamLimit = transform.position.y;
        _zCamLimit = transform.position.z;

        _startPositionRig = _cameraRig.transform.position;
    }

    private void Update()
    {
        // Сглаживание
        float _lerp = _smooth * Time.deltaTime;

        // Проверяем, нажал ли игрок на UI-элемент
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                if (IsTouchOverUIElement(_touch.position))
                    _isButtonPressed = true;
                else
                    _isButtonPressed = false;
            }
        }

        if (!_isButtonPressed)
        {
            // Двигаем камеру
            Movement();
            // Если игрок коснулся двумя пальцами, переносим камеру на начальную позицию
            if (Input.touchCount > 1)
            {
                StartCoroutine(MoveCameraToSatrt(_startPositionRig, _lerp));
                ResetTouchInput();
            }
            else
            {
                // Сглаживаем движение камеры
                _cameraRig.position = Vector3.Lerp(_cameraRig.position, _position, _lerp);
            }
            // Задаем границы для камеры
            _position.x = Mathf.Clamp(_position.x, _firstBorder, _secondBorder);
            _position.z = Mathf.Clamp(_position.z, _firstBorder, _secondBorder);

            // Вращаем камеру
            /*Rotation();
            cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, rotation, lerp);*/
        }
    }

    private void Movement()
    {
        if (Input.touchCount > 1)
        {
            _position = _startPositionRig;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _startPosition = GetNormalizedCameraInput();
        }
        else if (Input.GetMouseButton(0))
        {
            _position = _cameraRig.position;
            Vector3 _targetPosition = GetNormalizedCameraInput() - _startPosition;
            _position.x -= _targetPosition.x;
            _position.z -= _targetPosition.z;
        }
    }

    private IEnumerator MoveCameraToSatrt(Vector3 target, float lerp)
    {
        _cameraRig.position = Vector3.Lerp(_cameraRig.position, target, lerp);
        yield return null;
    }

    private void ResetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch _touch = Input.GetTouch(i);
            if (_touch.phase == TouchPhase.Began || _touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary || _touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                Input.ResetInputAxes();
                break;
            }
        }
    }

    private Vector3 GetNormalizedCameraInput()
    {
        if (_position.x > 5 || _position.x < -5 || _position.z > 5 || _position.z < -5)
        {
            Vector3 _screenPosition = Input.mousePosition * _movementSpeed;
            _screenPosition.z = _camera.nearClipPlane + 1;

            return _camera.ScreenToWorldPoint(_screenPosition);
        }
        else
        {
            Vector3 _screenPosition = Input.mousePosition * _movementSpeed;
            _screenPosition.z = _camera.nearClipPlane + 1;

            return _camera.ScreenToWorldPoint(_screenPosition);
        }
    }

    private bool IsTouchOverUIElement(Vector2 touchPosition)
    {
        // Создаем eventData для проверки пересечения с UI
        PointerEventData _eventData = new PointerEventData(EventSystem.current);
        _eventData.position = touchPosition;

        // Создаем список результатов пересечения
        var _results = new List<RaycastResult>();

        // Проверяем пересечение луча с UI-элементами
        EventSystem.current.RaycastAll(_eventData, _results);

        // Проверяем, есть ли результаты пересечения с UI-элементами
        return _results.Count > 0;
    }

    /*private void Rotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startRotation = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            float yRotation = (Input.mousePosition - startRotation).x * rotationSpeed;
            rotation *= Quaternion.Euler(new Vector3(0, yRotation, 0));
            startRotation = Input.mousePosition;
        }
    }*/
}
