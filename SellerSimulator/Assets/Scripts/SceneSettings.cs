using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    [SerializeField] private bool _isFpsCounterEnable;

    private float _fps;

    void Start()
    {
        _fpsText.gameObject.active = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        SetTestPreset();
#else
        SetNormalPreset();
#endif
    }

    private void Update()
    {
        // Счетчик фпс
        if (_isFpsCounterEnable)
        {
            if (!_fpsText.IsActive())
                _fpsText.gameObject.active = true;

            _fps = 1.0f / Time.deltaTime;
            _fpsText.text = "FPS: " + (int)_fps;
        }
        else
        {
            if (_fpsText.IsActive())
                _fpsText.gameObject.active = false;
        }
    }

    public static void ChangeGraphicSettings(bool isEcoModeEnable)
    {
        if (isEcoModeEnable)
            SetEcoPreset();
        else
            SetNormalPreset();
    }

    // Режим экономии энергии
    private static void SetEcoPreset()
    {
        // Настраиваем максимальный фпс
        Application.targetFrameRate = 30;

        // Меняем настройки графики
        QualitySettings.SetQualityLevel(0);
    }

    // Обычные игровые настройки без эко режима
    private static void SetNormalPreset()
    {
        // Настраиваем максимальный фпс
        Application.targetFrameRate = 120;

        // Меняем настройки графики
        QualitySettings.SetQualityLevel(1);
    }

    // Пресет для юнити эдитора (на компе)
    private static void SetTestPreset()
    {
        // Настраиваем максимальный фпс
        Application.targetFrameRate = 5000;

        // Меняем настройки графики
        QualitySettings.SetQualityLevel(3);
    }
}
