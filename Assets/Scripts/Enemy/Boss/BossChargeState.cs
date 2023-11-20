using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : BaseState
{
    Boss boss;
    private int stage;

    public float yOffset;
    public float chargeSpeed;
    public float chargeTime;
    public float waitTime;
    public int chargeNum;

    private int chargeCounter;
    private float chargeTimer;
    private float waitTimer;
    private GameObject player;
    private Vector2 teleportPoint;
    private Vector3 dir;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        chargeCounter = 0;
        chargeTimer = 0;
        waitTimer = 0;

        //These will be replaced by better codes later
        yOffset = 7;
        chargeSpeed = 3600;
        chargeTime = 1;
        waitTime = 0.3f;
        chargeNum = 3;
        
        //Find player
        player = GameObject.Find("player");
        teleportPoint = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
    }
    public override void LogicUpdate()
    {
        switch(stage)
        {
            case 0:
                boss.Teleport(teleportPoint);
                stage += 1;
                break;
            case 1:
                if(!boss.isTeleport) stage += 1;
                break;
            case 2:
                boss.isPreparingToCharge = true;
                boss.anim.SetTrigger("charge");
                stage += 1;
                break;
            case 3:
                if(!boss.isPreparingToCharge)
                {
                    dir = (player.transform.position - boss.transform.position) / (boss.transform.position - player.transform.position).magnitude;
                    boss.rb.velocity = chargeSpeed * Time.deltaTime * dir;
                    boss.GetComponent<Attack>().damage *= 2;
                    stage += 1;
                }
                break;
            case 4:
                chargeTimer += Time.deltaTime;
                if(chargeTimer >= chargeTime)
                {
                    boss.rb.velocity = new Vector3(0,0,0);
                    boss.GetComponent<Attack>().damage /= 2;
                    chargeTimer = 0;
                    stage += 1;
                }
                break;
            case 5:
                waitTimer += Time.deltaTime;
                if(waitTimer >= waitTime)
                {
                    waitTimer = 0;
                    chargeCounter += 1;
                    if(chargeCounter >= chargeNum)
                        boss.SwitchBossState(BossState.Wait);
                    else
                        stage = 2;
                }
                break;
            default:
                boss.SwitchBossState(BossState.Wait);
                break;
        }
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.Charge;

    }

    public override void PhysicsUpdate()
    {
    }
}
