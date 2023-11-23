using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrillianceState : BaseState
{
    Boss boss;
    private int stage;

    public Vector2 teleportPoint1;
    public Vector2 teleportPoint2;
    public float waitTime;
    public int shootNum;

    private float waitTimer;
    private int choice;
    private float angle;
    private BossBrillianceParameters para;

    public override void OnEnter(Enemy enemy)
    {
        //Debug.Log("Brilliant!");
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        waitTimer = 0;

        para = boss.GetComponent<BossBrillianceParameters>();
        waitTime = para.waitTime;
        teleportPoint1 = para.teleportPoint1;
        teleportPoint2 = para.teleportPoint2;
        shootNum = para.shootNum;
    }
    public override void LogicUpdate()
    {
        switch(stage)
        {
            case 0:
                choice = UnityEngine.Random.Range(0,2);
                if(choice == 0) boss.Teleport(teleportPoint1);
                else boss.Teleport(teleportPoint2);
                stage += 1;
                break;
            case 1:
                if(!boss.isTeleport) stage += 1;
                break;
            case 2:
                angle = UnityEngine.Random.Range(0f, (float)(2 * Math.PI / shootNum));
                for(int i = 0; i < shootNum; i++)
                {
                    GameObject light = UnityEngine.Object.Instantiate(boss.brilliance, boss.transform.position, Quaternion.identity);
                    light.GetComponent<BossLight>().angle = angle;
                    angle += (float)(2 * Math.PI / shootNum);
                }
                stage += 1;
                break;
            case 3:
                waitTimer += Time.deltaTime;
                if(waitTimer >= waitTime)
                {
                    waitTimer = 0f;
                    stage += 1;
                }
                break;
            case 4:
                if(choice == 1) boss.Teleport(teleportPoint1);
                else boss.Teleport(teleportPoint2);
                stage += 1;
                break;
            case 5:
                if(!boss.isTeleport) stage += 1;
                break;
            case 6:
                angle = UnityEngine.Random.Range(0f, (float)(2 * Math.PI / shootNum));
                for(int i = 0; i < shootNum; i++)
                {
                    GameObject light = UnityEngine.Object.Instantiate(boss.brilliance, boss.transform.position, Quaternion.identity);
                    light.GetComponent<BossLight>().angle = angle;
                    angle += (float)(2 * Math.PI / shootNum);
                }
                stage += 1;
                break;
            case 7:
                waitTimer += Time.deltaTime;
                if(waitTimer >= waitTime)
                {
                    waitTimer = 0f;
                    boss.SwitchBossState(BossState.Wait);
                }
                break;
            default:
                boss.SwitchBossState(BossState.Wait);
                break;
        }
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.Brilliance;

    }

    public override void PhysicsUpdate()
    {
    }
}
