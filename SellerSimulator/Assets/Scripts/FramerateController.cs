using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateController : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
