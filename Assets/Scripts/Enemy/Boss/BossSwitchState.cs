using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwitchState : BaseState
{
    Boss boss;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        boss.anim.SetTrigger("switchstage");
        boss.GetComponent<BossChargeParameters>().chargeSpeed = boss.GetComponent<BossStage2Parameters>().chargeSpeed;
        boss.GetComponent<BossChargeParameters>().waitTime = boss.GetComponent<BossStage2Parameters>().waitTime1;
        boss.GetComponent<BossChargeParameters>().chargeNum = boss.GetComponent<BossStage2Parameters>().chargeNum;
        boss.GetComponent<BossBarrage1Parameters>().shotInterval = boss.GetComponent<BossStage2Parameters>().shotInterval;
        boss.GetComponent<BossBarrage1Parameters>().shotNumber = boss.GetComponent<BossStage2Parameters>().shotNumber;
        boss.GetComponent<BossBarrage1Parameters>().shotSpeed = boss.GetComponent<BossStage2Parameters>().shotSpeed;
        boss.GetComponent<BossBarrage1Parameters>().rotationRate = boss.GetComponent<BossStage2Parameters>().rotationRate;
        boss.GetComponent<BossBarrage2Parameters>().attackInterval = boss.GetComponent<BossStage2Parameters>().attackInterval3;
        boss.GetComponent<BossBarrage2Parameters>().attackNum = boss.GetComponent<BossStage2Parameters>().attackNum3;
        boss.GetComponent<BossNebulaParameters>().attackInterval = boss.GetComponent<BossStage2Parameters>().attackInterval4;
        boss.GetComponent<BossNebulaParameters>().attackNum = boss.GetComponent<BossStage2Parameters>().attackNum4;
        boss.GetComponent<BossNebulaParameters>().bigBulletSpeed = boss.GetComponent<BossStage2Parameters>().speed4;
        boss.GetComponent<BossRockFallParameters>().baseSpeed = boss.GetComponent<BossStage2Parameters>().baseSpeed;
        boss.GetComponent<BossRockFallParameters>().rockInterval = boss.GetComponent<BossStage2Parameters>().rockInterval;
        boss.GetComponent<BossRockFallParameters>().rockNumber = boss.GetComponent<BossStage2Parameters>().rockNumber;
        boss.GetComponent<BossRockFallParameters>().horizontalLeftBound = boss.GetComponent<BossStage2Parameters>().horizontalLeftBound;
        boss.GetComponent<BossRockFallParameters>().horizontalRightBound = boss.GetComponent<BossStage2Parameters>().horizontalRightBound;
        boss.GetComponent<BossBrillianceParameters>().waitTime = boss.GetComponent<BossStage2Parameters>().waitTime6;
        boss.GetComponent<BossBrillianceParameters>().shootNum = boss.GetComponent<BossStage2Parameters>().shootNum;
    }
    public override void LogicUpdate()
    {
        if (boss.stage >= 2)
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
    }

    public override void PhysicsUpdate()
    {
    }
}
