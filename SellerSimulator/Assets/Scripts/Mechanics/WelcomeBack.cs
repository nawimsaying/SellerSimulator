using Assets.Scripts.Architecture.OnSaleFrame;
using Assets.Scripts.Architecture.WareHouse;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WelcomeBack : MonoBehaviour
{

    [SerializeField] private GameObject _popWindow;
    [SerializeField] private GameObject _background;


    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void DisplayInfo()
    {




        _popWindow.SetActive(true);
        _background.SetActive(true);

    }
}
