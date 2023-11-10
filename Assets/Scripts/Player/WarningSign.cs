using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    private GameObject sign;
    public int invisibleEnemyNum;
    // Start is called before the first frame update
    void Awake() 
    {
        sign = this.gameObject.transform.Find("WarningSign").gameObject;
        invisibleEnemyNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(invisibleEnemyNum > 0)
            sign.SetActive(true);
        else
            sign.SetActive(false);
        invisibleEnemyNum = 0;
    }
}
