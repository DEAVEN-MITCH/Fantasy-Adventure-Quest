using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitState : BaseState
{
    Boss boss;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        boss.wait = true;
        if(boss.GetComponent<Character>().currentHealth <= 0.5f * boss.GetComponent<Character>().maxHealth && boss.stage < 2)
            boss.SwitchBossState(BossState.SwitchStage);
        //todo
    }
    public override void LogicUpdate()
    {
        if (!boss.wait)
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
