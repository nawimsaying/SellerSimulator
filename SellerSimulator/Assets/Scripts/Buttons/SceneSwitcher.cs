using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // ����� ��� ����� ���� �� ������
    public void SceneSwitch(int idScene)
    {
        if (Input.touchCount == 1)
        {
            Touch touch = GetComponent<Touch>();
            int idTouch = touch.fingerId;

            //if ()
            //{
                SceneManager.LoadScene(idScene);

                CameraController.isButtonPressed = true;
            //}
        }
    }
}
