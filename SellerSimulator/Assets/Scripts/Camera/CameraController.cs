using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smooth = 10f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float firstBorder = -6f;
    [SerializeField] private float secondBorder = 6f;
    //[SerializeField] private float rotationSpeed = 0.2f;

    [NonSerialized] static public bool isButtonPressed = false;

    private Camera camera;
    private Transform cameraRig;

    private Vector3 position; // Позиция
    private Quaternion rotation; // Вращение
    private Vector3 localPosition; // Приблежение

    private Vector3 startPosition;
    private Vector3 startPositionRig;
    private Vector3 startRotation;

    private float yCamLimit;
    private float zCamLimit;

    private float xRotation = 45;
    private float yRotation = 161.6f;
    private int xRotationCurrent;

    void Start()
    {
        camera = Camera.main;
        cameraRig = transform.parent;

        position = cameraRig.position;
        rotation = cameraRig.rotation;
        localPosition = transform.localPosition;

        yCamLimit = transform.position.y;
        zCamLimit = transform.position.z;

        startPositionRig = cameraRig.transform.position;
    }

    private void Update()
    {
        // Сглаживание
        float lerp = smooth * Time.deltaTime;

        if (!isButtonPressed)
        {
            // Двигаем камеру
            Movement();
            // Если игрок коснулся двумя пальцами, переносим камеру на начальную позицию
            if (Input.touchCount > 1)
            {
                StartCoroutine(MoveCameraToSatrt(startPositionRig, lerp));
                ResetTouchInput();
            }
            else
            {
                // Сглаживаем движение камеры
                cameraRig.position = Vector3.Lerp(cameraRig.position, position, lerp);
            }
            // Задаем границы для камеры
            position.x = Mathf.Clamp(position.x, firstBorder, secondBorder);
            position.z = Mathf.Clamp(position.z, firstBorder, secondBorder);

            // Вращаем камеру
            /*Rotation();
            cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, rotation, lerp);*/
        }
    }

    private void Movement()
    {
        if (Input.touchCount > 1)
        {
            position = startPositionRig;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            startPosition = GetNormalizedCameraInput();
        }
        else if (Input.GetMouseButton(0))
        {
            position = cameraRig.position;
            Vector3 targetPosition = GetNormalizedCameraInput() - startPosition;
            position.x -= targetPosition.x;
            position.z -= targetPosition.z;
        }
    }

    private IEnumerator MoveCameraToSatrt(Vector3 target, float lerp)
    {
        cameraRig.position = Vector3.Lerp(cameraRig.position, target, lerp);
        yield return null;
    }

    private void ResetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Input.ResetInputAxes();
                break;
            }
        }
    }

    private Vector3 GetNormalizedCameraInput()
    {
        if (position.x > 5 || position.x < -5 || position.z > 5 || position.z < -5)
        {
            Vector3 screenPosition = Input.mousePosition * movementSpeed;
            screenPosition.z = camera.nearClipPlane + 1;

            return camera.ScreenToWorldPoint(screenPosition);
        }
        else
        {
            Vector3 screenPosition = Input.mousePosition * movementSpeed;
            screenPosition.z = camera.nearClipPlane + 1;

            return camera.ScreenToWorldPoint(screenPosition);
        }
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
