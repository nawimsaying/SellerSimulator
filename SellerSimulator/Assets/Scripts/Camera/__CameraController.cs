using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class __CameraController : MonoBehaviour
{
    [SerializeField] private float smooth = 10f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float mouseWheelSpeed = 1f;
    [SerializeField] private float zoomLevel = 1f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private Vector3 maxBounds;
    [SerializeField] private Vector3 minBounds;

    private Camera camera;
    private Transform cameraRig;

    private Vector3 position; // Позиция
    private Quaternion rotation; // Вращение
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
        rotation = cameraRig.rotation;
        localPosition = transform.localPosition;

        yCamLimit = transform.position.y;
        zCamLimit = transform.position.z;
    }

    private void Update()
    {
        float lerp = smooth * Time.deltaTime;

        Movement();
        transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -20, -1), camera.transform.position.y, Mathf.Clamp(camera.transform.position.z, 22, 37));
        cameraRig.position = Vector3.Lerp(cameraRig.position, position, lerp);

        RotationWithAcceleration();
        cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, rotation, lerp);
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

    private void RotationWithAcceleration()
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
    }

    private Vector3 GetNormalizedCameraInput()
    {
        Vector3 screenPosition = Input.mousePosition * movementSpeed;
        screenPosition.z = camera.nearClipPlane + 1;

        return camera.ScreenToWorldPoint(screenPosition);
    }
}
