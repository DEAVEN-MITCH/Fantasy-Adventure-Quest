using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SnailChaseState : BaseState
{
    private Snail snail;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        snail = (Snail)enemy;
        snail.cd = snail.throwtime;
        snail.lostCounter = snail.lostTime;
    }
    public override void LogicUpdate()
    {
        snail.ChangeDir();
        float distance = Vector3.Distance(snail.transform.position, snail.players.transform.position);
        float distance_1 = Vector3.Distance(snail.pointsForhit[0], snail.players.transform.position);
        float distance_2 = Vector3.Distance(snail.pointsForhit[1], snail.players.transform.position);
        float distance_3 = Vector3.Distance(snail.pointsForhit[2], snail.players.transform.position);
        snail.cd -= Time.deltaTime;
        if (distance_1 < distance_2 && distance_1 < distance_3)
        {
            snail.aim = snail.pointsForhit[0];
        }
        else if (distance_2 < distance_1 && distance_2 < distance_3)
        {
            snail.aim = snail.pointsForhit[1]+new Vector2(Random.Range(-snail.random,snail.random),0); 
        }
        else
        {
            snail.aim = snail.pointsForhit[2] + new Vector2(Random.Range(-snail.random, snail.random), 0);
        }
/*        if (distance > snail.checkDistance*1.5)
        {
            snail.SwitchState(NPCState.Patrol);
            return;
        }*/
        if(snail.cd<=0)
        {
            ThrowShoot();
            snail.cd = snail.throwtime;
        }
        if(snail.lostCounter<=0)
        {
            snail.SwitchState(NPCState.Patrol);
            return;
        }
        if (snail.players.transform.position.x - snail.transform.position.x >= 0)
            snail.sr.flipX = true;
        else
            snail.sr.flipX = false;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {
    }
    private void ThrowShoot()
    {
        // 生成石头
     


        // 计算抛物线运动参数
        float distanceX = snail.aim.x - snail.transform.position.x;
        float distanceY = snail.aim.y - snail.transform.position.y;
        float time = snail.flyTime;
        float speedX = distanceX / time*snail.speed_X;
        float speedY = (snail.gravity * time * time / 2+distanceY)/time;
/*        Debug.Log(speedX);
        Debug.Log(speedY);*/
        GameObject stone = Object.Instantiate(snail.bulletPrefab, snail.transform.position, Quaternion.identity);
/*        Object.Instantiate(snail.bulletPrefab, snail.transform.position, Quaternion.identity);*/
/*        Debug.Log(stone);*/
        Bullet bullet = stone.GetComponent<Bullet>();
        bullet.flyingSpeed = 0;
        Rigidbody2D stoneRb = stone.GetComponent<Rigidbody2D>();
        // 设置石头的初始速度
        stoneRb.velocity = new Vector2(speedX, speedY)
            ;
        stoneRb.gravityScale = snail.gravity;
    }
}
