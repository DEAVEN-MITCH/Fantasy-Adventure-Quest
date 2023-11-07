using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        enemy.anim.SetBool("walk", true);
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
        if (currentEnemy.hurtForce > 0) //This is a false contition; We need to change it later
        {
            //attack
            currentEnemy.anim.SetTrigger("attack");
            currentEnemy.SwitchState(NPCState.Skill);
            return;
        }
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);

    }

    public override void PhysicsUpdate()
    {
    }
}
