using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ComputerButtons : MonoBehaviour
{

    [SerializeField] private GameObject _canvasComputer;

    private void Start()
    {
        _canvasComputer.SetActive(true);
    }

    public void OpenCanvasComputer()
    {
        _canvasComputer.SetActive(true);
    }

    public void CloseCanvasComputer()
    {
        _canvasComputer.SetActive(false);
    }

}
