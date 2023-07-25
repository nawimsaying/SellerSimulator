using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WarehouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject _canvasAddPrefab;
    [SerializeField] private GameObject _canvasMain;
    [SerializeField] private GameObject _buttonEdit;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Vector3[] _spawnPosition;

    private static string[] _samplingFrame; // Responsible for whether the frame has a template or is it empty

    private static GameObject _buttonEditStatic;

    private bool _isSet = false; // Needed to keep track of whether the _isSamplingFrame array has been initialized

    private List<string> _namesOfSamples = new List<string>() { "rack", "pallet" }; // Template string name sheet

    private void Start()
    {
        StartCoroutine(SetValueSamplingFrame());

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);

        _buttonEditStatic = _buttonEdit;
    }

    // Responsible for pressing the plus button
    public void OpenCanvasAddPrefab()
    {
        _canvasMain.SetActive(false);
        _canvasAddPrefab.SetActive(true);
    }

    // Closes the canvas with templates
    public void CloseCanvasAddPrefab()
    {
        _canvasMain.SetActive(true);
        _canvasAddPrefab.SetActive(false);
    }

    // Spawn Template
    public void CreateSample(int idSample)
    {
        if (_samplingFrame[CameraWarehouse.GetCameraPosition()] == "none")
        {
            Instantiate(_prefabs[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // Indicate that there is already some template in this frame
            if (idSample == 0)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[0];
            else if (idSample == 1)
                _samplingFrame[CameraWarehouse.GetCameraPosition()] = _namesOfSamples[1];

            Debug.Log("Set sample - " + _samplingFrame[CameraWarehouse.GetCameraPosition()]);
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    // We initialize an array with templates using Curatin
    private IEnumerator SetValueSamplingFrame()
    {
        while (CameraWarehouse.countOfFrames == 0)
            yield return null;

        _samplingFrame = new string[CameraWarehouse.countOfFrames];

        // Crutch - when the player exits this scene, the location of his templates on the frames should be remembered
        // and the next time you switch to this scene, the isSamplingFrame array should have custom values
        // While filling the array with False values, which means that there are no templates in the frames
        for (int i = 0; i < _samplingFrame.Length; i++)
            _samplingFrame[i] = "none";

        Debug.Log("SF: " + _samplingFrame[0] + " " + _samplingFrame[1]);
    }
}
