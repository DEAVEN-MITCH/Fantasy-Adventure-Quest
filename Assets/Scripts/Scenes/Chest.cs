using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Chest : MonoBehaviour
{
    public string ChestID;
    public bool isGet;
    private TreasureTrigger chest;
    private DashTreasure chest2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Chest";
        ChestID = Guid.NewGuid().ToString();
        chest = GetComponent<TreasureTrigger>();
        chest2 = GetComponent<DashTreasure>();
    }
    private void Update()
    {
        if (chest != null)
        {
            isGet = chest.isGet;
        }
        if (chest2 != null)
        {
            isGet = chest2.isGet;
        }
    }
}
