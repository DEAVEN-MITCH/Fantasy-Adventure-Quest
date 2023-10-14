using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    public bool isGamePaused=false;
    public GameObject restartMenuCanvas;
    // 当对象已启用并处于活动状态时调用此函数
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
        // 获取当前场景的名称
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 重新加载当前场景
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
    // 暂停游戏
    public void PauseGame()
    {
        Time.timeScale = 0; // 设置时间缩放为0，暂停游戏
        isGamePaused = true;
    }

    // 恢复游戏
    public void ResumeGame()
    {
        Time.timeScale = 1; // 设置时间缩放为1，恢复游戏
        isGamePaused = false;
    }
    // 当行为被禁用或处于非活动状态时调用此函数
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

}
