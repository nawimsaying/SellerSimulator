using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ComputerButtons : MonoBehaviour
{

    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _canvasComputer;
    // Start is called before the first frame update
    private void Start()
    {
        _canvasMain.SetActive(true);
        _canvasComputer.SetActive(false);
    }

    public void OpenCanvasComputer()
    {
        _canvasMain.SetActive(false);
        _canvasComputer.SetActive(true);
    }

    public void CloseCanvasComputer()
    {
        _canvasMain.SetActive(true);
        _canvasComputer.SetActive(false);
    }

}
