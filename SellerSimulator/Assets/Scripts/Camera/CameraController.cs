using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _firstBorder = -6f;
    [SerializeField] private float _secondBorder = 6f;
    //[SerializeField] private float rotationSpeed = 0.2f;

    [NonSerialized] public static float smooth = 10f;
    [NonSerialized] public static bool isButtonPressed = false;

    private Camera _camera;
    private static Transform _cameraRig;

    private Vector3 _position; // Position
    private Quaternion _rotation; // Rotation
    private Vector3 _localPosition; // Zoom

    private Vector3 _startPosition;
    public static Vector3 startPositionRig;
    private Vector3 _startRotation;
    private bool _isClickerModeSwitched = false;

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

        startPositionRig = _cameraRig.transform.position;
    }

    private void Update()
    {
        // Smoothness
        float _lerp = smooth * Time.deltaTime;

        // Checking if the player clicked on the UI element
        if (!Clicker.isClickerModeEnable)
        {
            if (Input.touchCount > 0 && !_isClickerModeSwitched)
            {
                Touch _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began)
                {
                    if (IsTouchOverUIElement(_touch.position))
                        isButtonPressed = true;
                    else
                        isButtonPressed = false;
                }
            }

            if (!isButtonPressed)
            {
                // Moving the camera
                Movement();
                // If the player touches with two fingers, move the camera to the starting position
                if (Input.touchCount > 1)
                {
                    StartCoroutine(MoveCameraToStart(startPositionRig, _lerp));
                    ResetTouchInput();
                }
                else
                {
                    // Smoothing out camera movement
                    _cameraRig.position = Vector3.Lerp(_cameraRig.position, _position, _lerp);
                }
                // Set boundaries for the camera
                _position.x = Mathf.Clamp(_position.x, _firstBorder, _secondBorder);
                _position.z = Mathf.Clamp(_position.z, _firstBorder, _secondBorder);


                _isClickerModeSwitched = false;

                // Rotate the camera
                /*Rotation();
                cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, rotation, lerp);*/
            }
        }
        else
        {
            isButtonPressed = false;
            if (!_isClickerModeSwitched)
                StartCoroutine(MoveCameraToStart(startPositionRig, _lerp));
        }
    }

    private void Movement()
    {
        if (Input.touchCount > 1)
        {
            _position = startPositionRig;
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

    public IEnumerator MoveCameraToStart(Vector3 target, float lerp)
    {
        //_cameraRig.position = Vector3.Lerp(_cameraRig.position, target, lerp);
        float elapsedTime = 0f;
        float duration = 0.2f;
        Vector3 initialPosition = _cameraRig.position;

        while (elapsedTime < duration)
        {
            _cameraRig.position = Vector3.Lerp(initialPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            if (Clicker.isClickerModeEnable)
                _isClickerModeSwitched = true;

            yield return null;
        }
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
        // Create eventData to check for intersection with UI
        PointerEventData _eventData = new PointerEventData(EventSystem.current);
        _eventData.position = touchPosition;

        // Create a list of intersection results
        var _results = new List<RaycastResult>();

        // Checking for Ray Intersections with UI Elements
        EventSystem.current.RaycastAll(_eventData, _results);

        // Checking if there are intersection results with UI elements
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
