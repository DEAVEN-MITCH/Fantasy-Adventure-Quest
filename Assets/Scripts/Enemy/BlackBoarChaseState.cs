using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoarChaseState : BaseState
{
    private float acceleration;
    private float timeToMaxChaseSpeed=1;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        enemy.anim.SetBool("run", true);
        //currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostCounter = currentEnemy.lostTime;
        acceleration = (currentEnemy.chaseSpeed - currentEnemy.normalSpeed)/timeToMaxChaseSpeed;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.currentSpeed < currentEnemy.chaseSpeed)
        {
            
            currentEnemy.currentSpeed = Mathf.Min(currentEnemy.currentSpeed + acceleration*Time.deltaTime/timeToMaxChaseSpeed, currentEnemy.chaseSpeed);
        }
        //Debug.Log(currentEnemy.name + currentEnemy.currentSpeed);
        if (currentEnemy.lostCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
            return;
        }
        //´¥Ç½·­×ªÍ£Ö¹
        if (!currentEnemy.pc.isGround || currentEnemy.pc.touchLeftWall && currentEnemy.faceDir.x == -1 || currentEnemy.pc.touchRightWall && currentEnemy.faceDir.x == 1)
        {
            //currentEnemy.sr.flipX = !currentEnemy.sr.flipX;
            currentEnemy.SwitchState(NPCState.Halt);
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
