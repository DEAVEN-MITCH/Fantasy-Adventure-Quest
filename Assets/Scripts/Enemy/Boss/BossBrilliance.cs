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
            GameObject light = Object.Instantiate(boss.brilliance, boss.transform);
            light.GetComponent<BossLight>().angle = 0;
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
