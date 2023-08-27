using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Clicker : MonoBehaviour
{
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _canvasClickerMode;
    [SerializeField] private TextMeshProUGUI _clickerCounterText;

    [NonSerialized] public static bool isClickerModeEnable = false;

    private int _clickCount = 0;

    private void Update()
    {
        if (isClickerModeEnable)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                _clickCount++;

                _clickerCounterText.text = _clickCount.ToString();
            }
#else
            if (Input.touchCount < 4)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        _clickCount++;
                    }
                }
                _text.text = _clickCount.ToString();
            }
#endif
        }
    }

    public void ChangeToggleValue()
    {
        if (!isClickerModeEnable)
            ToggleOn();
        else
            ToggleOff();
    }

    // Logic when the player has enabled clicker mode
    private void ToggleOn()
    {
        isClickerModeEnable = true;

        _canvasMain.SetActive(false);
        _canvasClickerMode.SetActive(true);
    }

    // Logic when player turned off clicker mode
    private void ToggleOff()
    {
        _canvasClickerMode.SetActive(false);
        _canvasMain.SetActive(true);

        isClickerModeEnable = false;
    }
}
