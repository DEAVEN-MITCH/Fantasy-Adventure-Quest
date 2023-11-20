using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrillianceState : BaseState
{
    Boss boss;
    private int actTimes;  

    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("Brilliant!");
        currentEnemy = enemy;
        boss = (Boss)enemy;
        actTimes = 0;
    }
    public override void LogicUpdate()
    {
        if(actTimes == 0)
        {
            float angle = 0;
            for(int i = 0; i < 6; i++)
            {
                GameObject light = UnityEngine.Object.Instantiate(boss.brilliance, boss.transform.position, Quaternion.identity);
                light.GetComponent<BossLight>().angle = angle;
                angle += (float)(Math.PI / 3.0);
            }
            actTimes += 1;
        }
        else
            boss.SwitchBossState(BossState.Wait);
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.Brilliance;
        actTimes = 0;

    }

    public override void PhysicsUpdate()
    {
    }
}
