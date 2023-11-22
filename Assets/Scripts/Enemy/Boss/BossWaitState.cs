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
        //todo
    }
    public override void LogicUpdate()
    {
        if (!boss.wait)
        {
            //todo:add specific state switch rules
            switch (boss.lastAttackState)
            {
                case BossState.Charge:
                    boss.SwitchBossState(BossState.Brilliance);
                    break;
                case BossState.Barrage2:
                    boss.SwitchBossState(BossState.Nebula);
                    break;
                case BossState.Nebula:
                    boss.SwitchBossState(BossState.Barrage2);
                    break;
                case BossState.Brilliance:
                    boss.SwitchBossState(BossState.Charge);
                    break;
                default:
                    boss.SwitchBossState(boss.lastAttackState);
                    break;
            }
        }
    }

    public override void OnExit()
    {
        //todo

    }

    public override void PhysicsUpdate()
    {
    }
}
