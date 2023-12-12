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


    // ...��������...
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
        // �����浵��Ľ���
        if(isused)return;
        PhysicsCheck ps = collision.GetComponent<PhysicsCheck>();
        PlayerController pc = collision.GetComponent<PlayerController>();

        if (ps.isGround && !pc.isAttack && !pc.isHurt && !pc.pdc.isDashing && !pc.phc.isHeal && !pc.prc.isRebounce)
        {
            pc.inputControl.Gameplay.InteractE.started += SaveGame;
            // �ָ���ҵ�����������
            //player.currentPower = player.maxPower;
            //character.currentHealth = character.maxHealth;
            // ��ʾ��ʾ
            //uiManager.DisplayMessage("SUCCESSFULLY SAVED");
        }
        // ����������Ϸ״̬���߼�

        // �ڴ˴����������浵�㽻�����߼�������ָ����������������

        // ��ʾ��ʾ��������Ϣ

    }

    public void SaveGame(InputAction.CallbackContext obj)
    {
        if(isused)return;
        isused = true;
        
        Debug.Log("Save Game");
        // ��ȡ���λ��add
        currentGameData.playerSpawnPoint = transform.position;

        // ��ȡ����״̬
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript.isDead != true)
            {
                currentGameData.enemyList.Add(enemy);
                currentGameData.enemyStatus.Add(enemyScript.EnemyID, true);
            }
        }
        //��ȡ����״̬
        foreach (var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            var chestScript = chest.GetComponent<Chest>();
            currentGameData.chestStatus.Add(chestScript.ChestID, chestScript.isGet);
            currentGameData.chestList.Add(chest);
        }
        // ��ȡ�浵��״̬
        foreach (var save in GameObject.FindGameObjectsWithTag("SavePoint"))
        {
            var saveScript = save.GetComponent<SaveGameManager>();
            currentGameData.savePointStatus.Add(saveScript.SaveID, saveScript.isused);
        }
        currentGameData.playerScore = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().score;
        currentGameData.gameTime = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().startTime;

        // ��ȡ������ʱ��add
        UIManager uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        currentGameData.playerScore = uiManager.score;
        currentGameData.gameTime = uiManager.startTime;

        // ��ʾ��ʾ��������Ϣ
        //UIManager.instance.DisplayMessage("SUCCESSFULLY SAVED");

        // ������Ϸ����
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
        // ����Ƿ��д浵����
        if (currentGameData != null)
        {
            // �ָ�����״̬
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (currentGameData.enemyStatus.TryGetValue(enemyScript.EnemyID, out bool isAlive))
                {
                    enemyScript.isDead = !isAlive;
                }
            }


            // �ָ�����״̬
            GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
            foreach (var chest in chests)
            {
                Chest chestScript = chest.GetComponent<Chest>();
                if (currentGameData.chestStatus.TryGetValue(chestScript.ChestID, out bool isGet))
                {
                    chestScript.isGet = isGet;
                }
            }

            // �ָ����λ��
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = currentGameData.playerSpawnPoint;

            // �ָ�������ʱ��
            UIManager uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
            uiManager.score = currentGameData.playerScore;
            uiManager.startTime = currentGameData.gameTime;

            // �ָ��浵��״̬
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
    // ...��������...


}

