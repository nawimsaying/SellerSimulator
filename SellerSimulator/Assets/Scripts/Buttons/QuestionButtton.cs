using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : MonoBehaviour
{
    [SerializeField] private GameObject _questionCanvas;
    [SerializeField] private GameObject[] _anotherCanvas;

    private void Start()
    {
        _questionCanvas.SetActive(false);
    }

    public void OnQuestionEnter()
    {
        _questionCanvas.SetActive(true);

        foreach (var canvas in _anotherCanvas)
        {
            canvas.SetActive(false);
        }
    }

    public void OnQuestionExit()
    {
        Clicker clicker = new Clicker();

        _questionCanvas.SetActive(false);

        if (Clicker.isClickerModeEnable)
            _anotherCanvas[1].SetActive(true);
        else
            _anotherCanvas[0].SetActive(true);
    }
}
