using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossNebulaState : BaseState
{
    Boss boss;
    private int stage;

    public Vector2 teleportPoint;
    public float bigBulletSpeed;
    public float smallBulletSpeed;
    public float smallBulletRange;
    public float attackInterval;
    public int attackNum;

    private int attackCounter;
    private float shootTimer;
    private GameObject player;
    private Vector3 dir;
    private Vector3 lockedPosition;
    private BossNebulaParameters para;

    public override void OnEnter(Enemy enemy)
    {
        // ? These parameter values are only set for testing.
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;

        para = boss.GetComponent<BossNebulaParameters>();
        bigBulletSpeed = para.bigBulletSpeed;
        smallBulletSpeed = para.smallBulletSpeed;
        smallBulletRange = para.smallBulletRange;
        attackInterval = para.attackInterval;
        attackNum = para.attackNum;

        attackCounter = 0;
        shootTimer = 0;
        player = GameObject.Find("player");
        teleportPoint = para.teleportPoint;
    }
    public override void LogicUpdate()
    {
        switch (stage)
        {
            case 0:
                boss.Teleport(teleportPoint);
                stage += 1;
                break;
            case 1:
                if (!boss.isTeleport)
                    stage += 1;
                break;
            case 2:
                if (attackCounter > attackNum)
                {
                    stage += 1;
                    break;
                }
                lockedPosition = player.transform.position + new Vector3(0, 0.99f, 0);
                dir = (lockedPosition - boss.transform.position).normalized;
                shootTimer += Time.deltaTime;
                if (shootTimer >= attackInterval)
                {
                    GameObject bigBullet = Object.Instantiate(boss.nebula, boss.transform.position, Quaternion.LookRotation(Vector3.forward, dir) * Quaternion.Euler(0, 0, 90));
                    bigBullet.GetComponent<BossNebula>().targetPoint = lockedPosition;
                    bigBullet.GetComponent<BossNebula>().flyingSpeed = bigBulletSpeed;
                    bigBullet.GetComponent<BossNebula>().subBulletSpeed = smallBulletSpeed;
                    bigBullet.GetComponent<BossNebula>().subBulletRange = smallBulletRange;
                    shootTimer = 0;
                    attackCounter += 1;
                }
                break;
            default:
                boss.SwitchBossState(BossState.Wait);
                break;
        }
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.Nebula;
    }

    public override void PhysicsUpdate()
    {
    }
}
