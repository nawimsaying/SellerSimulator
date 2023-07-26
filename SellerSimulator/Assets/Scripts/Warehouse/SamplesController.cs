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

    private List<RackSample> _rackSamplesList = new List<RackSample>();

    private void Start()
    {
        _prefabsStatic = _prefabs;
    }

    public void CreateSample(int idSample)
    {
        if (!CheckFrameOnSamples())
        {
            Instantiate(_prefabsStatic[idSample], _spawnPosition[CameraWarehouse.GetCameraPosition()], Quaternion.Euler(0f, -90f, 0f));

            // Indicate that there is already some template in this frame
            if (idSample == 0) // Rack
            {
                int currentPosition = CameraWarehouse.GetCameraPosition();

                // Create an array for the rack
                RackSample newRackSample = new RackSample(currentPosition);
                _rackSamplesList.Add(newRackSample);
            }
            else if (idSample == 1) // Pallet
            {
                int currentPosition = CameraWarehouse.GetCameraPosition();
            }
        }
        else
            Debug.LogError("This frame already has a template installed.");
    }

    public void SetBox(int indexOfPlace)
    {
        int currentPosition = CameraWarehouse.GetCameraPosition();

        // Write down on the sheet where the box was placed
        _rackSamplesList[currentPosition].rackSample[indexOfPlace] = 1;

        Debug.Log("SetBox - Success!");

        // You still need to make these changes in memory and remember
    }

    private bool CheckFrameOnSamples()
    {
        int currentPosition = CameraWarehouse.GetCameraPosition();

        if (_rackSamplesList.Count > 0)
        {
            for (int i = 0; i < _rackSamplesList.Count; i++)
            {
                if (_rackSamplesList[i].idFrame == currentPosition)
                    return true;
            }
        }

        // Check big boxes list...

        return false;
    }
}
