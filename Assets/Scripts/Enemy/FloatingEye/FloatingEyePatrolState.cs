using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEyePatrolState : BaseState
{
    FloatingEye floatingEye;
    public override void LogicUpdate()
    {
        if (floatingEye.hurtSignal)
        {
            floatingEye.RandomMove();
            floatingEye.hurtSignal = false;
        }
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Skill);
        }

    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        floatingEye = (FloatingEye)enemy;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
