using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplesController : MonoBehaviour
{
    private static SamplesController _instance;

    public static SamplesController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SamplesController>();
            }
            return _instance;
        }
    }

    public class RackSample : MonoBehaviour
    {
        public int idFrame;
        public int[] rackSample;

        public RackSample(int currentCameraPosition)
        {
            idFrame = currentCameraPosition;

            rackSample = new int[21];

            for (int i = 0; i < 21; i++)
            {
                rackSample[i] = 0;
            }
        }
    }

    [SerializeField] private GameObject[] _prefabs;

    private static GameObject[] _prefabsStatic;

    private Vector3[] _spawnPosition = new Vector3[2] { new Vector3(0.09f, 0.27f, 3.94f), new Vector3(8f, 0.27f, 3.94f) };

    private static string[] _samplingFrame; // Responsible for whether the frame has a template or is it empty

    private List<string> _namesOfSamples = new List<string>() { "rack", "pallet" }; // Template string name sheet

    private List<RackSample> _rackSamplesList = new List<RackSample>();

    private void Start()
    {
        _prefabsStatic = _prefabs;

        StartCoroutine(SetValueSamplingFrame());
        _samplingFrame = new string[CameraWarehouse.countOfFrames];
    }

    public void CreateSample(int idSample)
    {
        if (_samplingFrame[CameraWarehouse.GetCameraPosition()] == "none")
        {
            Instantiate(_prefabsStatic[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // Indicate that there is already some template in this frame
            if (idSample == 0) // Rack
            {
                int currentPosition = CameraWarehouse.GetCameraPosition();

                // Spawn sample
                _samplingFrame[currentPosition] = _namesOfSamples[0];

                // Create an array for the rack
                RackSample newRackSample = new RackSample(currentPosition);
                _rackSamplesList.Add(newRackSample);
            }
            else if (idSample == 1) // Pallet
            {
                int currentPosition = CameraWarehouse.GetCameraPosition();

                // Spawn sample
                _samplingFrame[currentPosition] = _namesOfSamples[1];
            }

            Debug.Log("Set sample - " + _samplingFrame[CameraWarehouse.GetCameraPosition()]);
        }
        else
            Debug.Log("This frame already has a template installed.");
    }

    public void SetBox(int indexOfPlace)
    {
        int currentPosition = CameraWarehouse.GetCameraPosition();

        // Write down on the sheet where the box was placed
        _rackSamplesList[0].rackSample[indexOfPlace] = 1;

        // You still need to make these changes in memory and remember
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
