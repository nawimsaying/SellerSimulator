using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sliderText = null;

    private float _maxSliderAmount = 1f;

    public void ChangeMaxSliderAmount(int amount)
    {
        _maxSliderAmount = amount;
    }

    public void SliderChange(float value)
    {
        float localValue = value * _maxSliderAmount;
        _sliderText.text = localValue.ToString("0");
    }
}
