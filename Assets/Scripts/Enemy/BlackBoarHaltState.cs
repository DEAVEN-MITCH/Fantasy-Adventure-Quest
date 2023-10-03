using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoarHaltState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.wait = true;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.wait) return;
        else
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
    }
}
