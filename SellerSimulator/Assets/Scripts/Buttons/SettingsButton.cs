using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject[] _anotherCanvas;

    private void Start()
    {
        _settingsCanvas.SetActive(false);
    }

    public void OnSettingsEnter()
    {
        _settingsCanvas.SetActive(true);

        foreach (var canvas in _anotherCanvas) 
        {
            canvas.SetActive(false);
        }
    }

    public void OnSettingsExit()
    {
        Clicker clicker = new Clicker();

        _settingsCanvas.SetActive(false);

        if (Clicker.isClickerModeEnable)
            _anotherCanvas[1].SetActive(true);
        else
            _anotherCanvas[0].SetActive(true);
    }
}
