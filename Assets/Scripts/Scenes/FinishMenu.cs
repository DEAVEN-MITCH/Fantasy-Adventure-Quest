using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public GameObject StartCanvas;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
