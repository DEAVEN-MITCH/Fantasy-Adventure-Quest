using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    public bool isGamePaused=false;
    public GameObject restartMenuCanvas;
    // �����������ò����ڻ״̬ʱ���ô˺���
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

}
