using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossRockFallState : BaseState
{
    Boss boss;
    int stage;
    BossRockFallParameters parameters;
    float rockFallCountdown;
    int rockCount;
    float restCountdown;
    GameObject player;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        parameters = boss.GetComponent<BossRockFallParameters>();
        rockCount = 0;
        rockFallCountdown = parameters.rockInterval;
        restCountdown = parameters.restTime;
        player = GameObject.Find("player");
        if(boss.hardmode)
            player.GetComponent<PlayerDashController>().isDashUnlocked = false;
    }
    public override void LogicUpdate()
    {
        switch (stage)
        {
            case 0:
                boss.Teleport(parameters.teleportPosition);
                stage++;
                break;
            case 1:
                if (!boss.isTeleport)
                {
                    stage++;
                }
                break;
            case 2:
                if (rockCount >= parameters.rockNumber)
                {
                    stage++;break;
                }
                rockFallCountdown = Mathf.Max(0, rockFallCountdown - Time.deltaTime);
                if (rockFallCountdown == 0)
                {
                    rockFallCountdown = parameters.rockInterval;
                    GenerateRock();
                    rockCount++;
                }
                break;
            case 3:
                restCountdown = Mathf.Max(0, restCountdown - Time.deltaTime);
                if (restCountdown == 0)
                {
                    stage++;
                }
                break;
            default: boss.SwitchBossState(BossState.Wait);
                break;
        }
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.RockFall;
        if(boss.hardmode)
            player.GetComponent<PlayerDashController>().isDashUnlocked = true;
    }

    public override void PhysicsUpdate()
    {
    }
    void GenerateRock()
    {
        boss.StartCoroutine(AsynchronousRockFall());
    }
    IEnumerator AsynchronousRockFall()
    {
        Vector3 initialPosition;
        Vector3 playerPosition = player.transform.position;
        float xOffset = UnityEngine.Random.Range(parameters.horizontalLeftBound, parameters.horizontalRightBound);
        if(!boss.hardmode)
            initialPosition = playerPosition + new Vector3(xOffset, parameters.rockRelativeHeight, 0);
        else
        {
            float yOffset = parameters.rockRelativeHeight * (1- UnityEngine.Random.Range ((float)Math.Pow(Math.Abs(xOffset) / parameters.horizontalRightBound,2.0), (float)Math.Abs(xOffset) / parameters.horizontalRightBound));
            initialPosition = playerPosition + new Vector3(xOffset, yOffset, 0);
        }
        GameObject rock= GameObject.Instantiate(boss.rock, initialPosition, Quaternion.identity);
        if (!rock) yield break;
        if(!boss.hardmode)
        {rock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(parameters.stillTime);
        float f = UnityEngine.Random.Range(0f, 1f);
        bool isHalfSpeed = f <= parameters.halfSpeedPrbability;
        if (!rock) yield break;
        rock.GetComponent<Rigidbody2D>().velocity = new(0,-parameters.baseSpeed*(isHalfSpeed?.5f:2f));}
        yield break;
    }
}
