using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class _CameraController : MonoBehaviour
{
    [SerializeField] private float smooth = 10f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float mouseWheelSpeed = 1f;
    [SerializeField] private float zoomLevel = 1f;

    private Camera camera;
    private Transform cameraRig;

    private Vector3 position; // Позиция
    private Vector3 localPosition; // Приблежение

    private Vector3 startPosition;
    private Vector3 startRotation;

    private float yCamLimit;
    private float zCamLimit;

    private float xRotation = 45;
    private float yRotation = 161.6f;
    private int xRotationCurrent;

    private float startFOW = 18;
    private float currentFOW;
    private float targetFOW;

    private bool isButtonPressed = false;

    void Start()
    {
        camera = Camera.main;
        cameraRig = transform.parent;

        position = cameraRig.position;
        localPosition = transform.localPosition;

        yCamLimit = transform.position.y;
        zCamLimit = transform.position.z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Проверяем, нажал ли игрок на какой-нибудь UI-элемент
            if (EventSystem.current.IsPointerOverGameObject())
                isButtonPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
            isButtonPressed = false;


        // Если никакой UI-элемент не нажат, то двигаем камеру
        if (!isButtonPressed)
        {
            xRotationCurrent = (int)transform.localEulerAngles.x;

            float lerp = smooth * Time.deltaTime;

            Movement();
            cameraRig.position = Vector3.Lerp(cameraRig.position, position, lerp);

            if (Input.touchCount == 2 || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Touch touchFirst = Input.GetTouch(0);
                Touch touchSecond = Input.GetTouch(1);

                Vector2 touchFirstLastPosition = touchFirst.position - touchFirst.deltaPosition;
                Vector2 touchSecondLastPosition = touchSecond.position - touchSecond.deltaPosition;

                float distanceTouch = (touchFirstLastPosition - touchSecondLastPosition).magnitude;
                float currentDistanceTouch = (touchFirst.position - touchSecond.position).magnitude;

                float difference = currentDistanceTouch - distanceTouch;

                Zoom();

                transform.localPosition = Vector3.Lerp(transform.localPosition, localPosition, lerp);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(xRotation, yRotation, 0)), lerp);
            }
        }
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
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

    private Vector3 GetNormalizedCameraInput()
    {
        Vector3 screenPosition = Input.mousePosition * movementSpeed;
        screenPosition.z = camera.nearClipPlane + 1;

        return camera.ScreenToWorldPoint(screenPosition);
    }

    private void Zoom()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel < 0 && xRotationCurrent < 44 && xRotationCurrent > 35)
        {
            Zooming(mouseWheel);

            currentFOW = camera.fieldOfView;
            Debug.Log(currentFOW);

            if (currentFOW < 18)
                targetFOW = currentFOW + 0.5f;
            else
                targetFOW = currentFOW;

            float newFOW = Mathf.Lerp(startFOW, targetFOW, 1);
            camera.fieldOfView = newFOW;
        }
        
        if (mouseWheel > 0 && xRotationCurrent < 45 && xRotationCurrent > 35)
        {
            Zooming(mouseWheel);

            currentFOW = camera.fieldOfView;
            Debug.Log(currentFOW);

            if (currentFOW > 12)
                targetFOW = currentFOW - 0.5f;
            else
                targetFOW = currentFOW;

            float newFOW = Mathf.Lerp(startFOW, targetFOW, 1);
            camera.fieldOfView = newFOW;
        }

        if (mouseWheel < 0 && xRotationCurrent == 35)
        {
            Zooming(mouseWheel);

            currentFOW = camera.fieldOfView;
            Debug.Log(currentFOW);

            if (currentFOW < 18)
                targetFOW = currentFOW + 0.5f;
            else
                targetFOW = currentFOW;

            float newFOW = Mathf.Lerp(startFOW, targetFOW, 1);
            camera.fieldOfView = newFOW;
        }

        if (mouseWheel > 0 && xRotationCurrent == 45)
        {
            Zooming(mouseWheel);

            currentFOW = camera.fieldOfView;
            Debug.Log(currentFOW);

            if (currentFOW > 12)
                targetFOW = currentFOW - 0.5f;
            else
                targetFOW = currentFOW;

            float newFOW = Mathf.Lerp(startFOW, targetFOW, 1);
            camera.fieldOfView = newFOW;
        }
    }

    private void Zooming(float mouseWheel)
    {
        float distance = -mouseWheel * (transform.position.y / zoomLevel);
        xRotation = Mathf.Clamp(xRotation + distance, 35, 45);

        mouseWheel *= mouseWheelSpeed;
        localPosition += new Vector3(0, -mouseWheel, mouseWheel);

        localPosition.y = Mathf.Clamp(localPosition.y, zoomLevel, yCamLimit);
        localPosition.z = Mathf.Clamp(localPosition.z, zoomLevel, zCamLimit);
    }
}
