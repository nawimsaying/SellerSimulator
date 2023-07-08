using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraWarehouse : MonoBehaviour
{
    [SerializeField] private float smooth = 10f;
    [SerializeField] Vector3 holdDistance;
    [SerializeField] private float moveSpeed = 10f;

    private Camera camera;
    private Transform cameraRig;

    private Vector3 position; // Позиция

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private void Start()
    {
        camera = Camera.main;
        cameraRig = transform.parent;

        position = cameraRig.position;
    }

    private void Update()
    {
        // Сглаживание
        float lerp = smooth * Time.deltaTime;

        if (isMovingLeft)
        {
            // Вычисляем прогресс движения от 0 до 1
            float progress = Mathf.Clamp01(moveSpeed * Time.deltaTime / 6);

            // Используем Lerp для плавного сдвига объекта
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-6, 0f, 0f), progress);

            // Проверяем, достиг ли объект целевой дистанции
            if (progress >= 1.0f)
            {
                isMovingLeft = false;
            }
        }

        if (isMovingRight)
        {
            // Вычисляем прогресс движения от 0 до 1
            float progress = Mathf.Clamp01(moveSpeed * Time.deltaTime);

            Vector3 targetPosition = camera.transform.position + holdDistance;

            // Используем Lerp для плавного сдвига объекта
            transform.position = Vector3.Lerp(transform.position, targetPosition, progress);

            // Проверяем, достиг ли объект целевых координат
            isMovingRight = false;
        }
    }
    public void HoldLeft()
    {
        if(!isMovingLeft && !isMovingRight)
            isMovingLeft = true;
    }

    public void HoldRight()
    {
        if (!isMovingLeft && !isMovingRight)
            isMovingRight = true;
    }
}
