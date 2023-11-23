using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrage2State : BaseState
{
    Boss boss;
    private int stage;

    public Vector2 teleportPoint;
    public float bulletSpeed1;
    public float bulletSpeed2;
    public float bulletSpeed3;
    public float bulletRange;
    public float attackInterval;
    public int attackNum;

    private int attackCounter;
    private float shootTimer;
    private GameObject player;
    private Vector3 dir;
    private BossBarrage2Parameters para;

    public override void OnEnter(Enemy enemy)
    {
        // ? These parameter values are only set for testing.
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        para = boss.GetComponent<BossBarrage2Parameters>();
        bulletSpeed1 = para.bulletSpeed1;
        bulletSpeed2 = para.bulletSpeed2;
        bulletSpeed3 = para.bulletSpeed3;
        bulletRange = para.bulletRange;
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
                dir = (player.transform.position - boss.transform.position).normalized;
                shootTimer += Time.deltaTime;
                if (shootTimer >= attackInterval)
                {
                    Shoot(Quaternion.LookRotation(Vector3.forward, dir));
                    Shoot(Quaternion.LookRotation(Vector3.forward, dir) * Quaternion.Euler(0, 0, 45));
                    Shoot(Quaternion.LookRotation(Vector3.forward, dir) * Quaternion.Euler(0, 0, -45));
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
        boss.lastAttackState = BossState.Barrage2;
    }

    public override void PhysicsUpdate()
    {
    }

    /*
        @ Description: Shoot 3 bullets along the given direction.
        @ Parameters:
            Quaternion rotation: The given direction.
    */
    private void Shoot(Quaternion rotation)
    {
        GameObject bullet1 = Object.Instantiate(boss.barrage2, boss.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        GameObject bullet2 = Object.Instantiate(boss.barrage2, boss.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        GameObject bullet3 = Object.Instantiate(boss.barrage2, boss.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        bullet1.GetComponent<Bullet>().flyingSpeed = bulletSpeed1;
        bullet2.GetComponent<Bullet>().flyingSpeed = bulletSpeed2;
        bullet3.GetComponent<Bullet>().flyingSpeed = bulletSpeed3;

        // set attack range
        bullet1.GetComponent<Attack>().attackRange = bulletRange;
        bullet2.GetComponent<Attack>().attackRange = bulletRange;
        bullet3.GetComponent<Attack>().attackRange = bulletRange;
    }
}
