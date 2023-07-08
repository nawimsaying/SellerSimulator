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

    private Vector3 position; // �������

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
        // �����������
        float lerp = smooth * Time.deltaTime;

        if (isMovingLeft)
        {
            // ��������� �������� �������� �� 0 �� 1
            float progress = Mathf.Clamp01(moveSpeed * Time.deltaTime / 6);

            // ���������� Lerp ��� �������� ������ �������
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-6, 0f, 0f), progress);

            // ���������, ������ �� ������ ������� ���������
            if (progress >= 1.0f)
            {
                isMovingLeft = false;
            }
        }

        if (isMovingRight)
        {
            // ��������� �������� �������� �� 0 �� 1
            float progress = Mathf.Clamp01(moveSpeed * Time.deltaTime);

            Vector3 targetPosition = camera.transform.position + holdDistance;

            // ���������� Lerp ��� �������� ������ �������
            transform.position = Vector3.Lerp(transform.position, targetPosition, progress);

            // ���������, ������ �� ������ ������� ���������
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
