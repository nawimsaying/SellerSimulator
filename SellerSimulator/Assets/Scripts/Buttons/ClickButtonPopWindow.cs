using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickButtonPopWindow : MonoBehaviour
{
    public TextMeshProUGUI _counterText;

    // »змените _counter на static, чтобы он был общим дл€ всех экземпл€ров ClickButtonPopWindow
    public static int _counter = 1;

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
        if (_counter > 1)
        {
            _counter--;
            UpdateCounterText();
        }
    }
    public void ResetCounter()
    {
        _counter = 1;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        _counterText.text = _counter.ToString();
    }

}
