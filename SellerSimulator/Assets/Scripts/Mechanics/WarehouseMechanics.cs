using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarehouseMechanics : MonoBehaviour
{
    public static void CheckForObjectInteraction()
    {
        // �������� ������� ������� ������ �� ��������� ������ ��� ������� ���� � ������������ ������
        Vector3 _inputPosition;
#if UNITY_EDITOR || UNITY_STANDALONE
        _inputPosition = Input.mousePosition;
#else
        _inputPosition = Input.GetTouch(0).position;
#endif

        // ����������� ������� � ������� ����������
        Ray _ray = Camera.main.ScreenPointToRay(_inputPosition);
        RaycastHit _hit;

        // ������� ��� � ��������� ������������ � ����������� �������
        if (Physics.Raycast(_ray, out _hit))
        {
            // ���������, ��� ������������ ��������� � �������� � ������������ �����
            if (_hit.collider.CompareTag("SpaceForBox"))
            {
                // ��������� ������ ��������
                SpawnBox(_hit);
            }
        }
    }

    private static void SpawnBox(RaycastHit hit)
    {
        // ��� ��� ���������� ��������
        Vector3 _targetPosition = hit.collider.gameObject.transform.position;

        GameObject _instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate);

        _instantiatedPrefab.transform.position = hit.collider.gameObject.transform.position;

        Destroy(hit.collider.gameObject);
    }
}
