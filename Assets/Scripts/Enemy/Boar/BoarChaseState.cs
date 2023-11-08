using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        enemy.anim.SetBool("run", true);
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostCounter = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
/*        Debug.Log("chase");*/
        if (currentEnemy.lostCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
            return;
        }
        if (!currentEnemy.pc.isGround || currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1)
        {
            currentEnemy.sr.flipX = !currentEnemy.sr.flipX;
        }
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("run", false);

    }

    public override void PhysicsUpdate()
    {
    }
}
