using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLastAttackState : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        Boss boss = GetComponent<Boss>();
        boss.lastAttackState = number switch
        {
            1 => BossState.Charge,
            2 => BossState.Barrage1,
            3 => BossState.Barrage2,
            4 => BossState.Nebula,
            5 => BossState.RockFall,
            6 => BossState.Brilliance,
            _ => BossState.Wait
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
