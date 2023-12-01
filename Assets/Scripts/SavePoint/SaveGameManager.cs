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
    // ...��������...
    private void Awake()
    {
        gameObject.tag = "SavePoint";
        SaveID = Guid.NewGuid().ToString();
        currentGameData.playerSpawnPoint = GetComponent<Transform>().position;
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
        }
        // ����������Ϸ״̬���߼�

        // �ڴ˴����������浵�㽻�����߼�������ָ����������������

        // ��ʾ��ʾ��������Ϣ

    }

    public void SaveGame(InputAction.CallbackContext obj)
    {
        isused = true;
        Debug.Log("Save Game");
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
        // �ָ�����״̬

        // �����ָ��߼�...
    }

    // ...��������...
}

