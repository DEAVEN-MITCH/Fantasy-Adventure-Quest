using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector2 playerSpawnPoint; // 玩家存档的位置
    public List<GameObject> enemyList = new List<GameObject>();// 所有敌人的列表
    public List<GameObject> chestList = new List<GameObject>();// 所有宝箱的列表
    public Dictionary<string, bool> enemyStatus; // 所有敌人的存活状态
    public Dictionary<string, bool> chestStatus; // 所有宝箱的开启状态
    public int playerScore; // 玩家当前的分数
    public float gameTime; // 玩家当前的时间
    public Dictionary<string, bool> savePointStatus; // 所有存档点的可用状态
/*    public Dictionary<string, bool> hintStatus; // 所有提示的是否已触发状态*/

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
