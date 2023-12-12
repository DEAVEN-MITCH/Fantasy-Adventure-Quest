using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveSystem
{
    private static readonly string SaveKey = "GameData";

    public static void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static GameData LoadGameData()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }

    public static void ClearGameData()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
