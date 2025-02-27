using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public GameObject[] _prefabToInstantiate; // Prefab for instantiation

    [NonSerialized] public static bool isDrag = false;
    [NonSerialized] public static GameObject[] prefabToInstantiate;

    private GameObject _instantiatedPrefab; // Instantiated Prefab Object

    private void Start()
    {
        if (_prefabToInstantiate != null)
        {
            prefabToInstantiate = new GameObject[_prefabToInstantiate.Length];

            for (int i = 0; i < _prefabToInstantiate.Length; i++)
            {
                prefabToInstantiate[i] = _prefabToInstantiate[i];
            }
        }
    }

    // Fires when the cursor (finger) is pressed on the screen
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "ImageSmallBox")
        {
            // Setting a flag that the mouse button or finger on the screen is pressed
            isDrag = true;

            // Creating a prefab and linking it to the player's cursor
            _instantiatedPrefab = Instantiate(_prefabToInstantiate[0]);

            PlayerPrefs.SetString("dragging", "small");
        }
        else if (eventData.pointerEnter.gameObject.name == "ImageBigBox")
        {
            // Setting a flag that the mouse button or finger on the screen is pressed
            isDrag = true;

            // Creating a prefab and linking it to the player's cursor
            _instantiatedPrefab = Instantiate(_prefabToInstantiate[1]);

            PlayerPrefs.SetString("dragging", "big");
        }
        UpdatePrefabPosition();
    }

    // Fires when the cursor (finger) stops pressing the screen
    public void OnPointerUp(PointerEventData eventData)
    {
        // Setting a flag that the mouse button or finger on the screen is released
        isDrag = false;

        // Destroying an instantiated prefab
        Destroy(_instantiatedPrefab);

        WarehouseMechanics warehouseMechanics = new WarehouseMechanics();
        warehouseMechanics.CheckForObjectInteraction();
    }

    private void Update()
    {
        // If the mouse button or finger on the screen is pressed, update the position of the prefab
        if (isDrag)
            UpdatePrefabPosition();
    }

    private void UpdatePrefabPosition()
    {
        if (_instantiatedPrefab != null)
        {
            // Getting the position of the cursor or finger on the screen in world coordinates
            Vector3 _cursorPosition;
#if UNITY_EDITOR || UNITY_STANDALONE
            _cursorPosition = Input.mousePosition;
#else
        _cursorPosition = Input.GetTouch(0).position;
#endif

            // Convert the position of the cursor or finger on the screen to a position in game space
            _cursorPosition = Camera.main.ScreenToWorldPoint(_cursorPosition);
            // Adjusting the coordinates of the prefab so that the player keeps it in the middle
            _cursorPosition.z = 1;
            _cursorPosition.y = _cursorPosition.y - 1.3f;
            _cursorPosition.x = _cursorPosition.x - 0.55f;

            // Update the position of an instantiated prefab
            _instantiatedPrefab.transform.position = _cursorPosition;
        }
    }
}
