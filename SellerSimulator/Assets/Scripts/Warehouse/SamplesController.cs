using Assets.Scripts.Architecture.WareHouseDb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplesController : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    private static GameObject[] _prefabsStatic;

    private Vector3[] _spawnPosition = new Vector3[2]
    {
        new Vector3(0.09f, 0.27f, 3.94f),
        new Vector3(8f, 0.27f, 3.94f)
    };

    private void Start()
    {
        _prefabsStatic = _prefabs;

        //PlayerPrefs.DeleteKey("sampleList");
        //PlayerPrefs.DeleteKey("wareHouseDbMockList"); //��� �������� ������� �� ������
        //PlayerPrefs.DeleteKey("toolBarList");
        //PlayerPrefs.DeleteKey("onSaleFrameDbMockList"); //Mock for cancel item for on sale
        //PlayerPrefs.DeleteAll();

        PreparingTheScene();
    }

    public void CreateSample(int idSample)
    {
        if (!CheckFrameOnSamples())
        {
            List<Sample> sampleList = SaveLoadManager.LoadSampleList();
            List<SamplesOnFrames> samplesOnFramesList = SaveLoadManager.LoadSamplesOnFramesList();

            // Indicate that there is already some template in this frame
            int currentPosition = CameraWarehouse.GetCameraPosition();

            GameObject spawnObject = Instantiate(_prefabsStatic[idSample], _spawnPosition[currentPosition], _prefabsStatic[idSample].transform.rotation);
            spawnObject.name = GenerateSampleName();
            SamplesOnFrames samplesOnFrames = new SamplesOnFrames(currentPosition, spawnObject.name);
            samplesOnFramesList.Add(samplesOnFrames);
            SaveLoadManager.SaveSamplesOnFramesList(samplesOnFramesList);

            // Create an array for the sample
            Sample newRackSample = new Sample(currentPosition, idSample);
            sampleList.Add(newRackSample);

            // Save new sample
            SaveLoadManager.SaveSampleList(sampleList);
            sampleList = SaveLoadManager.LoadSampleList();
            Debug.Log(sampleList);
        }
        else
            Debug.LogWarning("This frame already has a template installed.");

        WarehouseButtons warehouseButtons = new WarehouseButtons();
        warehouseButtons.ChangePrefabActive();
    }

    public void DeleteSample()
    {
        if (CheckFrameOnSamples() && !CheckBoxesInFrame())
        {
            List<SamplesOnFrames> samplesOnFramesList = SaveLoadManager.LoadSamplesOnFramesList();
            List<Sample> sampleList = SaveLoadManager.LoadSampleList();
            int currentPosition = CameraWarehouse.GetCameraPosition();

            for (int i = 0; i < samplesOnFramesList.Count; i++)
            {
                if (samplesOnFramesList[i].idFrame == currentPosition)
                {
                    Destroy(GameObject.Find(samplesOnFramesList[i].sampleName));

                    for (int j = 0; j < sampleList.Count; j++)
                    {
                        if (sampleList[j].idFrame == currentPosition)
                        {
                            sampleList.Remove(sampleList[j]);

                            SaveLoadManager.SaveSampleList(sampleList);
                        }
                    }
                    samplesOnFramesList.Remove(samplesOnFramesList[i]);

                    SaveLoadManager.SaveSamplesOnFramesList(samplesOnFramesList);

                    break;
                }
            }
        }
        else
            Debug.LogWarning("There are boxes on the template, so it cannot be removed!");

        WarehouseButtons warehouseButtons = new WarehouseButtons();
        warehouseButtons.ChangePrefabActive();
    }

    public void SetBox(int indexOfPlace, ulong idBox)
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList();
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
        SaveLoadManager.SaveSampleList(sampleList);
    }

    private bool CheckFrameOnSamples()
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList();
        int currentPosition = CameraWarehouse.GetCameraPosition();

        for (int i = 0; i < sampleList.Count; i++)
        {
            if (sampleList[i].idFrame == currentPosition)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckBoxesInFrame()
    {
        List<Sample> sampleList = SaveLoadManager.LoadSampleList();
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

    private string GenerateSampleName()
    {
        int id = PlayerPrefs.GetInt("sampleName");

        id++;

        PlayerPrefs.SetInt("sampleName", id);

        return Convert.ToString(id);
    }

    private void PreparingTheScene()
    {
        List<SamplesOnFrames> samplesOnFramesList = SaveLoadManager.LoadSamplesOnFramesList();
        List<Sample> sampleList = SaveLoadManager.LoadSampleList();

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

            GameObject sample = Instantiate(_prefabsStatic[idSample], _spawnPosition[samplePosition], _prefabsStatic[idSample].transform.rotation);

            // Give name to sample
            for (int j = 0; j < samplesOnFramesList.Count; j++)
            {
                if (samplesOnFramesList[j].idFrame == samplePosition)
                    sample.name = samplesOnFramesList[j].sampleName;
            }

            // Get data
            WareHouseRepository houseRepository = new WareHouseRepository(new WareHouseDbMock());    
            List<ModelWareHouse> wareHouseRepositories = houseRepository.GetAll();

            // Spawn boxes on prefab
            for (int j = 0; j < sampleList[i].rackSample.Length; j++)
            {
                bool isSelled = true;

                if (sampleList[i].rackSample[j] != 0)
                {
                    for (int k = 0; k < wareHouseRepositories.Count; k++)
                    {
                        // If the box was not sold, spawn it
                        if (wareHouseRepositories[k].idBox == sampleList[i].rackSample[j])
                        {
                            GameObject instantiatedPrefab;

                            if (idSample == 0)
                                instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate[0]);
                            else
                                instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate[1]);

                            string nameOfSpace;

                            if (idSample == 0)
                            {
                                if (j < 10)
                                    nameOfSpace = "SpaceForBox" + "0" + Convert.ToString(j);
                                else
                                    nameOfSpace = "SpaceForBox" + Convert.ToString(j);
                            }
                            else
                            {
                                if (j < 10)
                                    nameOfSpace = "SpaceForBigBox" + "0" + Convert.ToString(j);
                                else
                                    nameOfSpace = "SpaceForBigBox" + Convert.ToString(j);
                            }

                            GameObject spaceForBox = sample.transform.Find(nameOfSpace).gameObject;

                            instantiatedPrefab.transform.position = spaceForBox.transform.position;

                            if (idSample == 1)
                                instantiatedPrefab.transform.rotation = spaceForBox.transform.rotation;

                            Destroy(spaceForBox);

                            isSelled = false;

                            break;
                        }
                    }
                    if (isSelled)
                    {
                        sampleList[i].rackSample[j] = 0;
                    }
                }
            }
            SaveLoadManager.SaveSampleList(sampleList);
        }
    }
}
