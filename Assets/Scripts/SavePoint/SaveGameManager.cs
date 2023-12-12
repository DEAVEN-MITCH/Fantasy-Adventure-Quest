using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class SaveGameManager : MonoBehaviour
{
    public bool isused = false;
    public GameData currentGameData;
    public string SaveID;
    public PlayerController player;
    public UIManager uiManager;
    public Character character;


    // ...其他代码...
    private void Awake()
    {
        gameObject.tag = "SavePoint";
        SaveID = Guid.NewGuid().ToString();
        currentGameData.playerSpawnPoint = GetComponent<Transform>().position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        character = GetComponent<Character>();
        RestoreGame();
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
            // 恢复玩家的生命和能量
            //player.currentPower = player.maxPower;
            //character.currentHealth = character.maxHealth;
            // 显示提示
            //uiManager.DisplayMessage("SUCCESSFULLY SAVED");
        }
        // 触发保存游戏状态的逻辑

        // 在此处添加其他与存档点交互的逻辑，例如恢复玩家生命和能量等

        // 显示提示或其他信息

    }

    public void SaveGame(InputAction.CallbackContext obj)
    {
        if(isused)return;
        isused = true;
        
        Debug.Log("Save Game");
        // 获取玩家位置add
        currentGameData.playerSpawnPoint = transform.position;

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
        //获取宝箱状态
        foreach (var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            var chestScript = chest.GetComponent<Chest>();
            currentGameData.chestStatus.Add(chestScript.ChestID, chestScript.isGet);
            currentGameData.chestList.Add(chest);
        }
        // 获取存档点状态
        foreach (var save in GameObject.FindGameObjectsWithTag("SavePoint"))
        {
            var saveScript = save.GetComponent<SaveGameManager>();
            currentGameData.savePointStatus.Add(saveScript.SaveID, saveScript.isused);
        }
        currentGameData.playerScore = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().score;
        currentGameData.gameTime = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().startTime;

        // 获取分数和时间add
        UIManager uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        currentGameData.playerScore = uiManager.score;
        currentGameData.gameTime = uiManager.startTime;

        // 显示提示或其他信息
        //UIManager.instance.DisplayMessage("SUCCESSFULLY SAVED");

        // 保存游戏数据
        //SaveSystem.SaveGameData(currentGameData);


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

        SaveSystem.SaveGameData(currentGameData);
    }

    public void RestoreGame()
    {
        // 检查是否有存档数据
        if (currentGameData != null)
        {
            // 恢复敌人状态
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (currentGameData.enemyStatus.TryGetValue(enemyScript.EnemyID, out bool isAlive))
                {
                    enemyScript.isDead = !isAlive;
                }
            }


            // 恢复宝箱状态
            GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
            foreach (var chest in chests)
            {
                Chest chestScript = chest.GetComponent<Chest>();
                if (currentGameData.chestStatus.TryGetValue(chestScript.ChestID, out bool isGet))
                {
                    chestScript.isGet = isGet;
                }
            }

            // 恢复玩家位置
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = currentGameData.playerSpawnPoint;

            // 恢复分数和时间
            UIManager uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
            uiManager.score = currentGameData.playerScore;
            uiManager.startTime = currentGameData.gameTime;

            // 恢复存档点状态
            GameObject[] savePoints = GameObject.FindGameObjectsWithTag("SavePoint");
            foreach (var savePoint in savePoints)
            {
                SaveGameManager savePointScript = savePoint.GetComponent<SaveGameManager>();
                if (currentGameData.savePointStatus.TryGetValue(savePointScript.SaveID, out bool isUsed))
                {
                    savePointScript.isused = isUsed;
                }
            }
        }
    }


    public void ClearSaveData()
    {
        SaveSystem.ClearGameData();
    }
    // ...其他代码...


}

