using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector2 playerSpawnPoint; // ��Ҵ浵��λ��
    public List<GameObject> enemyList = new List<GameObject>();// ���е��˵��б�
    public List<GameObject> chestList = new List<GameObject>();// ���б�����б�
    public Dictionary<string, bool> enemyStatus; // ���е��˵Ĵ��״̬
    public Dictionary<string, bool> chestStatus; // ���б���Ŀ���״̬
    public int playerScore; // ��ҵ�ǰ�ķ���
    public float gameTime; // ��ҵ�ǰ��ʱ��
    public Dictionary<string, bool> savePointStatus; // ���д浵��Ŀ���״̬
/*    public Dictionary<string, bool> hintStatus; // ������ʾ���Ƿ��Ѵ���״̬*/

    public GameData()
    {
        playerSpawnPoint = Vector2.zero;
        enemyStatus = new Dictionary<string, bool>();
        chestStatus = new Dictionary<string, bool>();
        playerScore = 0;
        gameTime = 0f;
        savePointStatus = new Dictionary<string, bool>();
    }
}
