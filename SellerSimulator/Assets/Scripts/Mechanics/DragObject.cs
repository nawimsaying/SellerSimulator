using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _prefabToInstantiate; // Префаб для инстанцирования

    [NonSerialized] public static bool isDrag = false;

    public static GameObject prefabToInstantiate;

    private GameObject _instantiatedPrefab; // Инстанцированный объект префаба

    private void Start()
    {
        if (_prefabToInstantiate != null)
            prefabToInstantiate = _prefabToInstantiate;
    }

    // Срабатывает, когда курсор (палец) нажимают на экран
    public void OnPointerDown(PointerEventData eventData)
    {
        // Установка флага, что кнопка мыши или палец на экране нажата
        isDrag = true;

        // Создание префаба и привязка его к курсору игрока
        _instantiatedPrefab = Instantiate(_prefabToInstantiate);

        UpdatePrefabPosition();
    }

    // Срабатывает, когда курсор (палец) перестают нажимать на экран
    public void OnPointerUp(PointerEventData eventData)
    {
        // Установка флага, что кнопка мыши или палец на экране отпущены
        isDrag = false;

        // Уничтожение инстанцированного префаба
        Destroy(_instantiatedPrefab);

        WarehouseMechanics.CheckForObjectInteraction();
    }

    private void Update()
    {
        // Если кнопка мыши или палец на экране нажата, обновляем позицию префаба
        if (isDrag)
            UpdatePrefabPosition();
    }

    private void UpdatePrefabPosition()
    {
        // Получение позиции курсора или пальца на экране в мировых координатах
        Vector3 _cursorPosition;
#if UNITY_EDITOR || UNITY_STANDALONE
        _cursorPosition = Input.mousePosition;
#else
        _cursorPosition = Input.GetTouch(0).position;
#endif
        
        // Преобразование позиции курсора или пальца на экране в позицию в пространстве игры
        _cursorPosition = Camera.main.ScreenToWorldPoint(_cursorPosition);
        // Корректируем координаты префаба так, чтобы игрок держал его посередине
        _cursorPosition.z = 1;
        _cursorPosition.y = _cursorPosition.y - 1.3f;
        _cursorPosition.x = _cursorPosition.x - 0.55f;

        // Обновление позиции инстанцированного префаба
        _instantiatedPrefab.transform.position = _cursorPosition;
    }
}
