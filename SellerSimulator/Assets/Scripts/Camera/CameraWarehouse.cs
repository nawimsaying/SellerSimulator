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
    [NonSerialized] public static int countOfFrames; // An analogue of _countOfFrames, only we will use it in other classes

    [SerializeField] private int _countOfFrames = 0; // Responsible for the number of frames in the current room
    [SerializeField] private float _speed = 2f; // Responsible for the speed of the camera when moving between frames
    [SerializeField] private float _totalDuration = 2f; // Responsible for the amount of smoothing of camera movement between frames
    [SerializeField] private GameObject _buttonLeft; // Specify the Left button
    [SerializeField] private GameObject _buttonRight; // Specify the Right button

    private Vector3 _moveDistanceLeft = new Vector3(7.2f, 0, 0);
    private Vector3 _moveDistanceRight = new Vector3(-7.2f, 0, 0);

    private bool _isMoving = false;

    public static bool[] _isCameraInFrame; // Using this array, we track the position of the camera and, accordingly, the current frame

    private void Start()
    {
        // Caching variables so they can be used in other classes
        countOfFrames = _countOfFrames;

        _isCameraInFrame = new bool[countOfFrames];

        // We fill the array with False values, which mean that there is no camera on this frame
        for (int i = 0; i < _isCameraInFrame.Length; i++) 
            _isCameraInFrame[i] = false;

        // At the start, we indicate that the camera is on the first frame
        _isCameraInFrame[0] = true;

        // Immediately deactivate the Left button, since we are on the leftmost frame
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

        _isMoving = false;
    }

    // A method with a mathematical function that is responsible for smoothing camera movement between frames
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    // After moving the camera between frames, enable or disable the Left or Right buttons
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
        // Are we moving now
        if (_isMoving)
            return;

        // Checking if we can move to the left
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i - 1 >= 0)
                {
                    // Move the camera to the left
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
        // Are we moving now
        if (_isMoving)
            return;

        // Checking if we can move to the right
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
            {
                if (i + 1 < _isCameraInFrame.Length)
                {
                    // Move the camera to the right
                    _isCameraInFrame[i] = false;
                    _isCameraInFrame[i + 1] = true;

                    StartCoroutine(MoveToTarget(_moveDistanceRight));
                }
                else
                    return;
            }
        }
    }

    // Method that returns the current camera position (what frame the player is currently on)
    public static int GetCameraPosition()
    {
        for (int i = 0; i < _isCameraInFrame.Length; i++)
        {
            if (_isCameraInFrame[i])
                return i; // Returning the index of the active frame
        }

        return -1; // In case of an error, return -1
    }
}
