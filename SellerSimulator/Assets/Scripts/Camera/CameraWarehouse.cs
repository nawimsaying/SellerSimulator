using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraWarehouse : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float totalDuration = 2f;
    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;

    private Vector3 moveDistanceLeft = new Vector3(6, 0, 0);
    private Vector3 moveDistanceRight = new Vector3(-6, 0, 0);
    private Transform target;

    private bool isMoving = false;

    private int[] frame_1 = { 0, 0 };

    private void Start()
    {
        frame_1[0] = 1;

        buttonLeft.SetActive(false);
    }

    private IEnumerator MoveToTarget(Vector3 moveDistance)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition - moveDistance;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float duration = journeyLength / speed;
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float normalizedTime = elapsedTime / duration;
            float easedTime = EaseInOut(normalizedTime);

            transform.position = Vector3.Lerp(startPosition, targetPosition, easedTime);

            elapsedTime = Time.time - startTime;

            yield return null;
        }

        ChangeActiveButtons();

        isMoving = false;
    }

    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    private void ChangeActiveButtons()
    {
        for (int i = 0; i < frame_1.Length; i++)
        {
            if (frame_1[i] == 1)
            {
                if (i - 1 >= 0)
                    buttonLeft.SetActive(true);
                else
                    buttonLeft.SetActive(false);

                if (i + 1 < frame_1.Length)
                    buttonRight.SetActive(true);
                else 
                    buttonRight.SetActive(false);
            }
        }
    }

    public void HoldLeft()
    {
        // Двигаемся ли мы сейчас
        if (isMoving)
            return;

        // Проверяем, можем ли мы двинуться влево
        for (int i = 0; i < frame_1.Length; i++)
        {
            if (frame_1[i] == 1)
            {
                if (i - 1 >= 0)
                {
                    frame_1[i] = 0;
                    frame_1[i - 1] = 1;

                    StartCoroutine(MoveToTarget(moveDistanceLeft));
                }
                else
                    return;
            }
        }
    }

    public void HoldRight()
    {
        // Двигаемся ли мы сейчас
        if (isMoving)
            return;

        // Проверяем, можем ли мы двинуться вправо
        for (int i = 0; i < frame_1.Length; i++)
        {
            if (frame_1[i] == 1)
            {
                if (i + 1 < frame_1.Length)
                {
                    frame_1[i] = 0;
                    frame_1[i + 1] = 1;

                    StartCoroutine(MoveToTarget(moveDistanceRight));
                }
                else
                    return;
            }
        }
    }
}
