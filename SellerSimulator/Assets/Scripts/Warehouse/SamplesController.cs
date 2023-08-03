using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SamplesController : MonoBehaviour
{
    //private static SamplesController _instance;

    //public static SamplesController Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<SamplesController>();
    //        }
    //        return _instance;
    //    }
    //}

    [SerializeField] private GameObject[] _prefabs;

    private static GameObject[] _prefabsStatic;
    private static List<SamplesOnFrames> _samplesOnFrames;

    private Vector3[] _spawnPosition = new Vector3[2] { new Vector3(0.09f, 0.27f, 3.94f), new Vector3(8f, 0.27f, 3.94f) };

    private List<Sample> _setedList = new List<Sample>();

    private void Start()
    {
        _prefabsStatic = _prefabs;
        _samplesOnFrames = SaveLoadManager.LoadSamplesOnFramesList("samplesOnFrameList");

        //PlayerPrefs.DeleteKey("sampleList");

        // Load RackSampleList from PlayerPrefs
        _setedList = SaveLoadManager.LoadSampleList("sampleList");

        // Arranging objects in the scene
        PreparingTheScene();
    }

    public void CreateSample(int idSample)
    {
        if (!CheckFrameOnSamples())
        {
            // Indicate that there is already some template in this frame
            int currentPosition = CameraWarehouse.GetCameraPosition();

            GameObject spawnObject = Instantiate(_prefabsStatic[idSample], _spawnPosition[currentPosition], Quaternion.Euler(0f, -90f, 0f));
            SamplesOnFrames samplesOnFrames = new SamplesOnFrames(currentPosition, spawnObject);
            _samplesOnFrames.Add(samplesOnFrames);
            SaveLoadManager.SaveSamplesOnFramesList("samplesOnFrameList", _samplesOnFrames);

            // Create an array for the sample
            Sample newRackSample = new Sample(currentPosition, idSample);
            _setedList.Add(newRackSample);

            // Save new sample
            SaveLoadManager.SaveSampleList("sampleList", _setedList);
            _setedList = SaveLoadManager.LoadSampleList("sampleList");
            Debug.Log(_setedList);
        }
        else
            Debug.LogError("This frame already has a template installed.");
    }

    public void DeleteSample()
    {
        if (CheckFrameOnSamples() && !CheckBoxesInFrame())
        {
            //Destroy();
        }
    }

    public void SetBox(int indexOfPlace, ulong idBox)
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList("sampleList");
        int currentPosition = CameraWarehouse.GetCameraPosition();

        // Write down on the sheet where the box was placed
        for (int i = 0; i < sampleList.Count; i++)
        {
            if (sampleList[i].idFrame == currentPosition)
            {
                sampleList[i].rackSample[indexOfPlace] = idBox;
                break;
            }
        }
        SaveLoadManager.SaveSampleList("sampleList", sampleList);
    }

    private bool CheckFrameOnSamples()
    { 
        List<Sample> sampleList = SaveLoadManager.LoadSampleList("sampleList");
        int currentPosition = CameraWarehouse.GetCameraPosition();

        for (int i = 0; i < sampleList.Count; i++)
        {
            if (sampleList[i].idFrame == currentPosition)
            {
                for (int j = 0; j < sampleList[i].rackSample.Length; j++)
                {
                    if (sampleList[i].rackSample[j] != 0)
                        return true;
                }
            }
        }
        return false;
    }

    private bool CheckBoxesInFrame()
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList("sampleList");
        int currentPosition = CameraWarehouse.GetCameraPosition();

        for (int i = 0; i < sampleList.Count; i++)
        {
            for (int j = 0; j < sampleList[i].rackSample.Length; j++)
            {
                if (sampleList[i].rackSample[j] != 0)
                    return true;
            }
        }
        return false;
    }

    private void PreparingTheScene()
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList("sampleList");

        // Spawn all samples
        for (int i = 0; i < sampleList.Count; i++)
        {
            // Where
            int samplePosition = sampleList[i].idFrame;

            // What
            int idSample;

            if (sampleList[i].rackSample.Length == 21)
                idSample = 0;
            else
                idSample = 1;

            GameObject sample = Instantiate(_prefabsStatic[idSample], _spawnPosition[samplePosition], Quaternion.Euler(0f, -90f, 0f));

            // Spawn boxes on prefab
            for (int j = 0; j < sampleList[i].rackSample.Length; j++)
            {
                if (sampleList[i].rackSample[j] != 0)
                {
                    GameObject instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate);

                    string nameOfSpace;

                    if (j < 10)
                        nameOfSpace = "SpaceForBox" + "0" + Convert.ToString(j);
                    else
                        nameOfSpace = "SpaceForBox" + Convert.ToString(j);

                    GameObject spaceForBox = sample.transform.Find(nameOfSpace).gameObject;

                    instantiatedPrefab.transform.position = spaceForBox.transform.position;

                    Destroy(spaceForBox);
                }
            }
        }
    }
}
