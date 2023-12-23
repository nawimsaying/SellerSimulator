using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SceneSwitch(int _idScene)
    {
        InfoBoxCells.BarBoxUpdate();
        SceneManager.LoadScene(_idScene);
    }
}
