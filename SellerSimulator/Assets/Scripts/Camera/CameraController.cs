using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private Vector3 touchStart; // касание экрана
    public Camera cam;
    public float groundZ = 0; // начальная точка при перемещении камеры
    public Vector3 minBounds; // мин граница координат 
    public Vector3 maxBounds; // макс граница координат
    private bool isButtonPressed = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Проверяем, нажал ли игрок на какой-нибудь UI-элемент
            if (EventSystem.current.IsPointerOverGameObject())
                isButtonPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isButtonPressed = false;
        }

        // Если никакой UI-элемент не нажат, то двигаем камеру
        if (!isButtonPressed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = GetWorldPosition(groundZ);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - GetWorldPosition(groundZ);
                cam.transform.position += direction;
            }

            LimitCameraPosition();
        }
    }

    private Vector3 GetWorldPosition(float z)
    {
        float distance;
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void LimitCameraPosition()
    {
        Vector3 newPosition = cam.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);
        cam.transform.position = newPosition;
    }
}
