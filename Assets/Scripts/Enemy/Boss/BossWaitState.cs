using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitState : BaseState
{
    Boss boss;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        boss.wait = true;
        //todo
    }
    public override void LogicUpdate()
    {
        if (!boss.wait)
        {
            int choice = Random.Range(0,2);
            switch (boss.lastAttackState)
            {
                case BossState.Charge:
                    if(choice == 0) boss.SwitchBossState(BossState.Barrage1);
                    else boss.SwitchBossState(BossState.Barrage2);
                    break;
                case BossState.Barrage1:
                    if(choice == 0) boss.SwitchBossState(BossState.Barrage2);
                    else boss.SwitchBossState(BossState.Nebula);
                    break;
                case BossState.Barrage2:
                    if(choice == 0) boss.SwitchBossState(BossState.Nebula);
                    else boss.SwitchBossState(BossState.RockFall);
                    break;
                case BossState.Nebula:
                    if(choice == 0) boss.SwitchBossState(BossState.RockFall);
                    else boss.SwitchBossState(BossState.Brilliance);
                    break;
                case BossState.RockFall:
                    if(choice == 0) boss.SwitchBossState(BossState.Brilliance);
                    else boss.SwitchBossState(BossState.Charge);
                    break;
                case BossState.Brilliance:
                    if(choice == 0) boss.SwitchBossState(BossState.Charge);
                    else boss.SwitchBossState(BossState.Barrage1);
                    break;
                default:
                    if(choice == 0) boss.SwitchBossState(BossState.Brilliance);
                    else boss.SwitchBossState(BossState.Barrage2);
                    break;
            }
        }
    }

    public override void OnExit()
    {
        //not good, but better than none
        if(boss.GetComponent<Character>().currentHealth <= 0.5f * boss.GetComponent<Character>().maxHealth)
        {
            boss.GetComponent<BossChargeParameters>().chargeSpeed = 5000;
            boss.GetComponent<BossChargeParameters>().waitTime = 0.2f;
            boss.GetComponent<BossChargeParameters>().chargeNum = 5;
            boss.GetComponent<BossBarrage1Parameters>().shotInterval = 0.16f;
            boss.GetComponent<BossBarrage1Parameters>().shotNumber = 35;
            boss.GetComponent<BossBarrage1Parameters>().shotSpeed = 360;
            boss.GetComponent<BossBarrage1Parameters>().rotationRate = 90;
            boss.GetComponent<BossBarrage2Parameters>().attackInterval = 0.75f;
            boss.GetComponent<BossBarrage2Parameters>().attackNum = 12;
            boss.GetComponent<BossNebulaParameters>().attackInterval = 0.6f;
            boss.GetComponent<BossNebulaParameters>().attackNum = 15;
            boss.GetComponent<BossRockFallParameters>().baseSpeed = 10;
            boss.GetComponent<BossRockFallParameters>().rockInterval = 0.21f;
            boss.GetComponent<BossRockFallParameters>().rockNumber = 35;
            boss.GetComponent<BossBrillianceParameters>().waitTime = 0;
            boss.GetComponent<BossBrillianceParameters>().shootNum = 8;
        }
    }

    public override void PhysicsUpdate()
    {
    }
}
