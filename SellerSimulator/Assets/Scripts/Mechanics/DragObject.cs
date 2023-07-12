using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _prefabToInstantiate; // ������ ��� ���������������

    [NonSerialized] public static bool isDrag = false;

    public static GameObject prefabToInstantiate;

    private GameObject _instantiatedPrefab; // ���������������� ������ �������

    private void Start()
    {
        if (_prefabToInstantiate != null)
            prefabToInstantiate = _prefabToInstantiate;
    }

    // �����������, ����� ������ (�����) �������� �� �����
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��������� �����, ��� ������ ���� ��� ����� �� ������ ������
        isDrag = true;

        // �������� ������� � �������� ��� � ������� ������
        _instantiatedPrefab = Instantiate(_prefabToInstantiate);

        UpdatePrefabPosition();
    }

    // �����������, ����� ������ (�����) ��������� �������� �� �����
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��������� �����, ��� ������ ���� ��� ����� �� ������ ��������
        isDrag = false;

        // ����������� ����������������� �������
        Destroy(_instantiatedPrefab);

        WarehouseMechanics.CheckForObjectInteraction();
    }

    private void Update()
    {
        // ���� ������ ���� ��� ����� �� ������ ������, ��������� ������� �������
        if (isDrag)
            UpdatePrefabPosition();
    }

    private void UpdatePrefabPosition()
    {
        // ��������� ������� ������� ��� ������ �� ������ � ������� �����������
        Vector3 _cursorPosition;
#if UNITY_EDITOR || UNITY_STANDALONE
        _cursorPosition = Input.mousePosition;
#else
        _cursorPosition = Input.GetTouch(0).position;
#endif
        
        // �������������� ������� ������� ��� ������ �� ������ � ������� � ������������ ����
        _cursorPosition = Camera.main.ScreenToWorldPoint(_cursorPosition);
        // ������������ ���������� ������� ���, ����� ����� ������ ��� ����������
        _cursorPosition.z = 1;
        _cursorPosition.y = _cursorPosition.y - 1.3f;
        _cursorPosition.x = _cursorPosition.x - 0.55f;

        // ���������� ������� ����������������� �������
        _instantiatedPrefab.transform.position = _cursorPosition;
    }
}
