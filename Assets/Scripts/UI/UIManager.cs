using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("Attributes")]
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    public bool isGamePaused=false;
    public GameObject restartMenuCanvas;
    // 
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character arg0)
    {
        var percentage = arg0.currentHealth / arg0.maxHealth;
        playerStateBar.OnHealthChange(percentage);
    }
    public void RestartGame()
    {
        //restartMenuCanvas.SetActive("false");
        // 
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 
        SceneManager.LoadScene(currentSceneName);
        ResumeGame();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
        ResumeGame();

    }
    public void ShowRestartMenu()
    {
        //Debug.Log("ShowRestatMenu");
        restartMenuCanvas.SetActive(true);
        PauseGame();
    }
    // 
    public void PauseGame()
    {
        Time.timeScale = 0; 
        isGamePaused = true;
    }

    // 
    public void ResumeGame()
    {
        Time.timeScale = 1; // 
        isGamePaused = false;
    }
    // 
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

}
