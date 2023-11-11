using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : BaseState
{
    Snail snail;
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            return;
        }
        if (currentEnemy.lostCounter <= 0)
        {
            currentEnemy.sr.flipX = !currentEnemy.sr.flipX;
            currentEnemy.lostCounter = currentEnemy.lostTime;
        }
        //
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        snail = (Snail)enemy;
        currentEnemy.lostCounter = currentEnemy.lostTime;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
