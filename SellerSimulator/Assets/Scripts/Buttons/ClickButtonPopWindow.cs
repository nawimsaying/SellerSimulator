using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClickButtonPopWindow : MonoBehaviour
{
    public TextMeshProUGUI _counterText;

    private int _counter = 0;

    private void Start()
    {
        UpdateCounterText();
    }

    public void PlusCounter()
    {
        _counter++;
        UpdateCounterText();
    }

    public void MinesCounter()
    {
        if (_counter > 0)
        {
            _counter--;
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        _counterText.text = $"{_counter} רע.";
    }
}
