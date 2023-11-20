using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrillianceState : BaseState
{
    Boss boss;
    private int stage;
    private float angle;

    public Vector2 teleportPoint1;
    public Vector2 teleportPoint2;
    public float waitTime;

    private float waitTimer;
    private int choice;

    public override void OnEnter(Enemy enemy)
    {
        //Debug.Log("Brilliant!");
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        waitTimer = 0;

        //These will be replaced by better codes later
        waitTime = 1;
        teleportPoint1 = new Vector2(-37, 111);
        teleportPoint2 = new Vector2(-22.5f, 111);
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
                angle = UnityEngine.Random.Range(0f, (float)(Math.PI / 3.0));
                for(int i = 0; i < 6; i++)
                {
                    GameObject light = UnityEngine.Object.Instantiate(boss.brilliance, boss.transform.position, Quaternion.identity);
                    light.GetComponent<BossLight>().angle = angle;
                    angle += (float)(Math.PI / 3.0);
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
                angle = UnityEngine.Random.Range(0f, (float)(Math.PI / 3.0));
                for(int i = 0; i < 6; i++)
                {
                    GameObject light = UnityEngine.Object.Instantiate(boss.brilliance, boss.transform.position, Quaternion.identity);
                    light.GetComponent<BossLight>().angle = angle;
                    angle += (float)(Math.PI / 3.0);
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
