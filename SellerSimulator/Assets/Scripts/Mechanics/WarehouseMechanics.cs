using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarehouseMechanics : MonoBehaviour
{
    public static void CheckForObjectInteraction()
    {
        // Получаем позицию касания пальца на сенсорном экране или позицию мыши в пространстве экрана
        Vector3 _inputPosition;
#if UNITY_EDITOR || UNITY_STANDALONE
        _inputPosition = Input.mousePosition;
#else
        _inputPosition = Input.GetTouch(0).position;
#endif

        // Преобразуем позицию в мировые координаты
        Ray _ray = Camera.main.ScreenPointToRay(_inputPosition);
        RaycastHit _hit;

        // Пускаем луч и проверяем столкновение с коллайдером объекта
        if (Physics.Raycast(_ray, out _hit))
        {
            // Проверяем, что столкновение произошло с объектом с определенным тэгом
            if (_hit.collider.CompareTag("SpaceForBox"))
            {
                // Выполняем нужное действие
                SpawnBox(_hit);
            }
        }
    }

    private static void SpawnBox(RaycastHit hit)
    {
        // Код для выполнения действия
        Vector3 _targetPosition = hit.collider.gameObject.transform.position;

        GameObject _instantiatedPrefab = Instantiate(DragObject.prefabToInstantiate);

        _instantiatedPrefab.transform.position = hit.collider.gameObject.transform.position;

        Destroy(hit.collider.gameObject);
    }
}
