using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartingState : BaseState
{
    Boss boss;
    float speed;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        boss.transform.position = new Vector3(-30,124.2f,0);
        boss.stage = 1;
        speed = 3000;
        boss.isStarting = true;
        boss.anim.SetTrigger("start");
        boss.rb.velocity = new Vector3(0, -1, 0) * speed * Time.deltaTime;
        
        boss.character.invulnerable = true;
        boss.character.invulnerableCounter = 100;
        boss.c2.excludeLayers = boss.teleportLayermask;
    }
    public override void LogicUpdate()
    {
        speed *= 0.99f;
        boss.rb.velocity = new Vector3(0, -1, 0) * speed * Time.deltaTime;
        if (!boss.isStarting)
        {
            boss.rb.velocity = new Vector3(0,0,0);
            int choice = Random.Range(0,2);
            if(choice == 0) boss.SwitchBossState(BossState.Brilliance);
            else boss.SwitchBossState(BossState.Barrage2);
        }
    }

    public override void OnExit()
    {
        boss.character.invulnerable = false;
        boss.character.invulnerableCounter = 0;
        boss.c2.excludeLayers = boss.normalLayermask;
        boss.onStart.Invoke();
    }

    public override void PhysicsUpdate()
    {
    }
}
