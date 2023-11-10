using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject SelectCanvas;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowSelectScene()
    {
        StartCanvas.SetActive(false);
        SelectCanvas.SetActive(true);
    }
    public void ShowStartMenu()
    {
        SelectCanvas.SetActive(false);
        StartCanvas.SetActive(true);
    }
    public void StartLevel(int level)
    {
        SceneManager.LoadScene("Level"+level);
    }
}
