using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class SaveGameManager : MonoBehaviour
{
    public bool isused = false;
    public GameData currentGameData;
    public string SaveID;
    // ...其他代码...
    private void Awake()
    {
        gameObject.tag = "SavePoint";
        SaveID = Guid.NewGuid().ToString();
        currentGameData.playerSpawnPoint = GetComponent<Transform>().position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测与存档点的交互
        if(isused)return;
        PhysicsCheck ps = collision.GetComponent<PhysicsCheck>();
        PlayerController pc = collision.GetComponent<PlayerController>();

        if (ps.isGround && !pc.isAttack && !pc.isHurt && !pc.pdc.isDashing && !pc.phc.isHeal && !pc.prc.isRebounce)
        {
            pc.inputControl.Gameplay.InteractE.started += SaveGame;
        }
        // 触发保存游戏状态的逻辑

        // 在此处添加其他与存档点交互的逻辑，例如恢复玩家生命和能量等

        // 显示提示或其他信息

    }

    public void SaveGame(InputAction.CallbackContext obj)
    {
        isused = true;
        Debug.Log("Save Game");
        // 获取敌人状态
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript.isDead != true)
            {
                currentGameData.enemyList.Add(enemy);
                currentGameData.enemyStatus.Add(enemyScript.EnemyID, true);
            }
        }
        foreach (var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            var chestScript = chest.GetComponent<Chest>();
            currentGameData.chestStatus.Add(chestScript.ChestID, chestScript.isGet);
            currentGameData.chestList.Add(chest);
        }
        foreach (var save in GameObject.FindGameObjectsWithTag("SavePoint"))
        {
            var saveScript = save.GetComponent<SaveGameManager>();
            currentGameData.savePointStatus.Add(saveScript.SaveID, saveScript.isused);
        }
        currentGameData.playerScore = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().score;
        currentGameData.gameTime = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().startTime;
        foreach (var kvp in currentGameData.chestStatus)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
        foreach (var kvp in currentGameData.enemyStatus)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
        foreach (var kvp in currentGameData.savePointStatus)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }

    public void RestoreGame()
    {
        // 恢复敌人状态

        // 其他恢复逻辑...
    }

    // ...其他代码...
}

