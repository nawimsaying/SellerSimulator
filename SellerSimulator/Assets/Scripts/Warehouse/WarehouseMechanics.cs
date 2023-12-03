using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class WarehouseMechanics : MonoBehaviour
{
    public void CheckForObjectInteraction()
    {
        // Get the touch position of the finger on the touch screen or the position of the mouse in screen space
        Vector3 _inputPosition;

#if UNITY_EDITOR || UNITY_STANDALONE
        _inputPosition = Input.mousePosition;
#else
        _inputPosition = Input.GetTouch(0).position;
#endif

        // Convert position to world coordinates
        Ray _ray = Camera.main.ScreenPointToRay(_inputPosition);
        RaycastHit _hit;

        // Launch the beam and check for collision with the object's collider
        if (Physics.Raycast(_ray, out _hit))
        {
            // Checking that the collision happened with an object with a specific tag
            if (_hit.collider.CompareTag("SpaceForBox") && PlayerPrefs.GetString("dragging") == "small")
            {
                // Update count of boxes in tool bar
                int smallBoxes = PlayerPrefs.GetInt("smallBoxes");
                smallBoxes--;
                PlayerPrefs.SetInt("smallBoxes", smallBoxes);

                ToolBarList _toolBarList = SaveLoadManager.LoadToolBarList();

                ulong idBox = 0;
                for (int i = 0; i < _toolBarList.toolBarList.Count; i++)
                {
                    if (_toolBarList.toolBarList[i].sizeBox == "Small")
                    {
                        idBox = (ulong)i;
                        break;
                    }
                }

                // Convert to int may be a problem
                _toolBarList.toolBarList.Remove(_toolBarList.toolBarList[(int)idBox]);

                WarehouseData warehouseData = new WarehouseData();
                var item = warehouseData.GetSaveSnapshotToolBarList(_toolBarList.toolBarList);
                SaveLoadManager.SaveToolBarList(warehouseData.GetSaveSnapshotToolBarList(_toolBarList.toolBarList));

                // Performing the required action
                SpawnBox(_hit, idBox);

                WarehouseButtons warehouseButtons = new WarehouseButtons();
                warehouseButtons.SpawnBoxesInToolBar();
            }
            else if (_hit.collider.CompareTag("SpaceForBigBox") && PlayerPrefs.GetString("dragging") == "big")
            {
                // Update count of boxes in tool bar
                int bigBoxes = PlayerPrefs.GetInt("bigBoxes");
                bigBoxes--;
                PlayerPrefs.SetInt("bigBoxes", bigBoxes);

                ToolBarList _toolBarList = SaveLoadManager.LoadToolBarList();

                ulong idBox = 0;
                for (int i = 0; i < _toolBarList.toolBarList.Count; i++)
                {
                    if (_toolBarList.toolBarList[i].sizeBox == "Big")
                    {
                        idBox = (ulong)i;
                        break;
                    }
                }

                // Convert to int may be a problem
                _toolBarList.toolBarList.Remove(_toolBarList.toolBarList[(int)idBox]);

                WarehouseData warehouseData = new WarehouseData();
                var item = warehouseData.GetSaveSnapshotToolBarList(_toolBarList.toolBarList);
                SaveLoadManager.SaveToolBarList(warehouseData.GetSaveSnapshotToolBarList(_toolBarList.toolBarList));

                // Performing the required action
                SpawnBox(_hit, idBox);

                WarehouseButtons warehouseButtons = new WarehouseButtons();
                warehouseButtons.SpawnBoxesInToolBar();
            }
        }
        PlayerPrefs.SetString("dragging", "none");
    }

    private static void SpawnBox(RaycastHit hit, ulong idBox)
    {
        if (hit.collider.CompareTag("SpaceForBox"))
        {
            // Code to perform an action
            GameObject instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate[0]);
            GameObject spaceForBox = hit.collider.gameObject;

            SamplesController samplesController = new SamplesController();

            string spaceForBoxName = spaceForBox.name;

            string indexOfSpaceForBox = spaceForBoxName.Substring(spaceForBoxName.Length - 2);

            int index = int.Parse(EditIndex(indexOfSpaceForBox));

            samplesController.SetBox(index, idBox);

            instantiatedPrefab.transform.position = spaceForBox.transform.position;

            Destroy(spaceForBox);
        }
        else if (hit.collider.CompareTag("SpaceForBigBox"))
        {
            // Code to perform an action
            GameObject instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate[1]).gameObject;
            GameObject spaceForBox = hit.collider.gameObject;

            SamplesController samplesController = new SamplesController();

            string spaceForBoxName = spaceForBox.name;

            string indexOfSpaceForBox = spaceForBoxName.Substring(spaceForBoxName.Length - 2);

            int index = int.Parse(EditIndex(indexOfSpaceForBox));

            samplesController.SetBox(index, idBox);

            instantiatedPrefab.transform.position = spaceForBox.transform.position;

            instantiatedPrefab.transform.rotation = spaceForBox.transform.rotation;

            Destroy(spaceForBox);
        }
    }

    private static string EditIndex(string index)
    {
        char[] temp = new char[index.Length];

        for (int i = 0; i < index.Length; i++)
            temp[i] = index[i];

        if (temp[0] == 0)
            return Convert.ToString(temp[1]);
        
        return index;
    }
}
