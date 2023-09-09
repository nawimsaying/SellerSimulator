using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ComputerPopWindowController : MonoBehaviour
{
    [SerializeField] private GameObject popWindow;
    [SerializeField] private GameObject background;

    private void Start()
    {
        popWindow.SetActive(false);
        background.SetActive(false);
    }

    /*public void OpenPopWindow()
    {
        popWindow.SetActive(true);
        background.SetActive(true);
    }*/

    public void ClosePopWindow()
    {
        popWindow.SetActive(false);
        background.SetActive(false);
    }
}
