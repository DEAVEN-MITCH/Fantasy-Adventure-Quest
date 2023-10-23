using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public ScoreEventSO scoreEvent;
    public PlayerStateBar playerStateBar;
    public bool isGamePaused=false;
    public GameObject restartMenuCanvas;
    public GameObject pauseMenuCanvas;
    public TMP_Text timeRenderer;
    public TMP_Text scoreRenderer;
    private int score=0;
    private float startTime;
    // �����������ò����ڻ״̬ʱ���ô˺���
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        scoreEvent.OnEventRaised += OnScoreChange;
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        UpdateTimeRenderer();
    }

    private void OnHealthEvent(Character arg0)
    {
        var percentage = arg0.currentHealth / arg0.maxHealth;
        playerStateBar.OnHealthChange(percentage);
    }
    public void RestartGame()
    {
        //restartMenuCanvas.SetActive("false");
        // ��ȡ��ǰ����������
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���¼��ص�ǰ����
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
    // ��ͣ��Ϸ
    public void PauseGame()
    {
        Time.timeScale = 0; // ����ʱ������Ϊ0����ͣ��Ϸ
        isGamePaused = true;
    }

    // �ָ���Ϸ
    public void ResumeGame()
    {
        Time.timeScale = 1; // ����ʱ������Ϊ1���ָ���Ϸ
        isGamePaused = false;
    }
    // ����Ϊ�����û��ڷǻ״̬ʱ���ô˺���
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }
    public void ShowPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        PauseGame();
    }
    public void ExitPauseMenu()
    {
        pauseMenuCanvas.SetActive(false);
        ResumeGame();
    }
    public void UpdateTimeRenderer()
    {
        float totalSeconds = Time.time - startTime;
        //int hours = (int)totalSeconds / 3600; 
        int minutes = (int)(totalSeconds % 3600) / 60;
        int seconds = (int)(totalSeconds % 60);
        timeRenderer.SetText(String.Format("Time:{0:00}:{1:00}", minutes, seconds));
    }
    private void UpdateScoreRenderer()
    {
        scoreRenderer.SetText(String.Format("Score:{0:d}", score));
    }
    public void OnScoreChange(int change)
    {
        score += change;
        if (score <0) score = 0;
        UpdateScoreRenderer();
    }
}
