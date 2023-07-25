using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Text _text;

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

                _text.text = _clickCount.ToString();
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
        if (_toggle.isOn)
            ToggleOn();
        else
            ToggleOff();
    }

    // Logic when the player has enabled clicker mode
    private void ToggleOn()
    {
        isClickerModeEnable = true;

        _canvas.SetActive(false);
    }

    // Logic when player turned off clicker mode
    private void ToggleOff()
    {
        _canvas.SetActive(true);

        isClickerModeEnable = false;
    }
}
