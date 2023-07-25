using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class WarehouseMechanics : MonoBehaviour
{
    public static void CheckForObjectInteraction()
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
            if (_hit.collider.CompareTag("SpaceForBox"))
            {
                // Performing the required action
                SpawnBox(_hit);
            }
        }
    }

    private static void SpawnBox(RaycastHit hit)
    {
        // Code to perform an action
        GameObject instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate);
        GameObject spaceForBox = hit.collider.gameObject;

        SamplesController samplesController = new SamplesController();

        string spaceForBoxName = spaceForBox.name;

        string indexOfSpaceForBox = spaceForBoxName.Substring(spaceForBoxName.Length - 2);

        int index = int.Parse(EditIndex(indexOfSpaceForBox));

        samplesController.SetBox(index);

        instantiatedPrefab.transform.position = spaceForBox.transform.position;

        Destroy(spaceForBox);
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
