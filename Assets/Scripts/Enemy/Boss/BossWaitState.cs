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
            boss.SwitchBossState(boss.lastAttackState);
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
