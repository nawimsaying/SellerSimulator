using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private bool _isFpsCounterEnable;
    [SerializeField] private TextMeshProUGUI _screenText;

    private float _fps;

    // Dynamic Resolution
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

        // Turn off text with fps by default
        _fpsText.gameObject.active = false;

        // Set the graphics preset and fps limit depending on the platform
#if UNITY_EDITOR || UNITY_STANDALONE
        SetTestPreset();
#else
        SetNormalPreset();
#endif
    }

    private void Update()
    {
        // FPS counter
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
        
        // Dynamic Resolution
        if (Time.time > _delayTime)
        {
            // Reducing resolution
            if (Time.deltaTime > _minFpsFloat)
            {
                if (_currentScale > _minScale)
                {
                    _currentScale -= _scaleStep;
                    Screen.SetResolution((int)(_mainResolution.x * _currentScale), (int)(_mainResolution.y * _currentScale), true);
                    _delayTime = Time.time + _delay;
                }
            }
            // Resolution increase
            /*else if (CurScale < 1 && Time.deltaTime < MaxFPSS)
            {
                CurScale += ScaleStep;
                Screen.SetResolution((int)(MainRes.x * CurScale), (int)(MainRes.y * CurScale), true);
                DelayTime = Time.time + Delay;
            }*/
            _delayTime = Time.time + 0.5f;
        }
        _screenText.text = "MainRes: " + _mainResolution + " X: " + Screen.width + " / Y: " + Screen.height + " / CurScale " + _currentScale + " / ";
    }

    public static void ChangeGraphicSettings(bool isEcoModeEnable)
    {
        if (isEcoModeEnable)
            SetEcoPreset();
        else
            SetNormalPreset();
    }

    // Energy saving mode
    private static void SetEcoPreset()
    {
        // Setting the maximum fps
        Application.targetFrameRate = 30;

        // Changing graphics settings
        QualitySettings.SetQualityLevel(0);
    }

    // Normal gaming settings without eco mode
    private static void SetNormalPreset()
    {
        // Setting the maximum fps
        Application.targetFrameRate = 120;

        // Changing graphics settings
        QualitySettings.SetQualityLevel(1);
    }

    // Unit editor preset (on a computer)
    private static void SetTestPreset()
    {
        // Setting the maximum fps
        Application.targetFrameRate = 5000;

        // Changing graphics settings
        QualitySettings.SetQualityLevel(3);
    }
}
