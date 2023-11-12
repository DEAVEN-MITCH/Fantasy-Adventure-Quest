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
            snail.aim = snail.pointsForhit[1];
        }
        else
        {
            snail.aim = snail.pointsForhit[2];
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
        // ����ʯͷ
     


        // �����������˶�����
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
        stone.GetComponent<Bullet>().flyingSpeed = 0;
        stone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
        stone.GetComponent<Rigidbody2D>().gravityScale = snail.gravity;

        for(int i = 1; i < 3; i++)
        {
            stone = Object.Instantiate(snail.bulletPrefab, snail.transform.position, Quaternion.identity);
            stone.GetComponent<Bullet>().flyingSpeed = 0;
            stone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX * Random.Range(0.7f,1.3f), speedY * Random.Range(0.7f,1.3f));
            stone.GetComponent<Rigidbody2D>().gravityScale = snail.gravity;
        }
    }
}
