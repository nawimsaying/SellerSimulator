using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // ����� ��� ����� ���� �� ������
    public void SceneSwitch(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }
}
