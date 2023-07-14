using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText;

    private float fps;

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        fpsText.text = "FPS: " + (int)fps;
    }
}
