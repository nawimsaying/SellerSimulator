using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    [SerializeField] private bool _isFpsCounterEnable;
    [SerializeField] private Text screenText;

    private float _fps;

    // Динамическое разрешение
    private Vector2 _mainResolution;
    private float _currentScale = 1;
    private float _minScale = 0.5f;
    private float _scaleStep = 0.05f;
    private int _minFps = 30;
    private int _maxFps = 60;
    private float _minFpsFloat;
    private float _maxFpsFloat;
    private float _delay;
    private float _delayTime;


    void Start()
    {
        _mainResolution = new Vector2(Screen.width, Screen.height);
        _delayTime = _delay;
        _minFpsFloat = 1f / (float)_minFps;
        _maxFpsFloat = 1f / (float)_maxFps;

        // По умолчанию отключаем текст с фпс
        _fpsText.gameObject.active = false;

        // Устанавливаем пресет графики и ограничение фпс в зависимости от платформы
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

        // Динамическое разрешение
        if (Time.time > _delayTime)
        {
            // Уменьшение разрешения
            if (Time.deltaTime > _minFpsFloat)
            {
                if (_currentScale > _minScale)
                {
                    _currentScale -= _scaleStep;
                    Screen.SetResolution((int)(_mainResolution.x * _currentScale), (int)(_mainResolution.y * _currentScale), true);
                    _delayTime = Time.time + _delay;
                }
            }
            // Увеличение разрешения
            /*else if (CurScale < 1 && Time.deltaTime < MaxFPSS)
            {
                CurScale += ScaleStep;
                Screen.SetResolution((int)(MainRes.x * CurScale), (int)(MainRes.y * CurScale), true);
                DelayTime = Time.time + Delay;
            }*/
            _delayTime = Time.time + 0.5f;
        }
        screenText.text = "MainRes: " + _mainResolution + "X: " + Screen.width + " / Y: " + Screen.height + " / CurScale " + _currentScale + " / ";
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
